using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Execise.Middleware;



var builder = WebApplication.CreateBuilder(args);

// 1. Register services
builder.Services.AddControllers(options =>
{
    // Exercise 7: Global Audit Action Filter
    options.Filters.Add<AuditActionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Exercise 5: Scoped service for concurrency experiment
builder.Services.AddScoped<IRequestAuditTracker, RequestAuditTracker>();

// Exercise 6: Authentication & Authorization setup (JWT Bearer example)
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

// ============================================================================
// HTTP REQUEST PIPELINE (Order matters — documented for Exercise 8)
// ============================================================================

// Exercise 1 & 3: Correlation ID first so TraceIdentifier and log scope apply everywhere
app.UseMiddleware<CorrelationIdMiddleware>();

// Exercise 2 & 3: Request timing right after CorrelationId so logs carry the CorrelationId
app.UseMiddleware<RequestTimingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Exercise 4: Branch /admin with ApiKeyMiddleware (short-circuits with 401 if missing/invalid)
app.Map("/admin", adminApp =>
{
    adminApp.UseMiddleware<ApiKeyMiddleware>();
    adminApp.UseRouting();
    adminApp.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/stats", (HttpContext ctx) => Results.Ok(new
        {
            message = "Admin access granted",
            correlationId = ctx.TraceIdentifier
        }));
    });
});

app.UseRouting();

// Exercise 6: Authentication MUST precede Authorization
app.UseAuthentication();
app.UseAuthorization();

// Exercise 5: Switch between GoodScopedInvokeMiddleware (per-request) and
// BadScopedConstructorMiddleware (captive scoped dependency) to observe concurrency behavior
app.UseMiddleware<GoodScopedInvokeMiddleware>();

// Exercise 5 test endpoint: overlaps two concurrent calls with a 1.5s delay
app.MapGet("/diagnostics/scoped-test", async (
    IRequestAuditTracker tracker,
    string? label,
    CancellationToken ct) =>
{
    var beforeOwner = tracker.CurrentOwner;
    tracker.CurrentOwner = label ?? $"req-{tracker.InstanceId.ToString()[..6]}";

    await Task.Delay(1500, ct);

    return Results.Ok(new
    {
        tracker.InstanceId,
        beforeOwner,
        afterOwner = tracker.CurrentOwner
    });
});

// Exercise 6 test endpoint: requires authenticated user
app.MapGet("/secure/profile", [Authorize] (HttpContext ctx) => Results.Ok(new
{
    user = ctx.User.Identity?.Name ?? "authenticated-user",
    correlationId = ctx.TraceIdentifier
}));

app.MapControllers();

app.Run();

public interface IRequestAuditTracker
{
    Guid InstanceId { get; }
    string? CurrentOwner { get; set; }
}

public sealed class RequestAuditTracker : IRequestAuditTracker
{
    public Guid InstanceId { get; } = Guid.NewGuid();
    public string? CurrentOwner { get; set; }
}

// ANTI-PATTERN (Exercise 5 Part A): Injecting scoped service into constructor
public sealed class BadScopedConstructorMiddleware(
    RequestDelegate next,
    IRequestAuditTracker scopedTracker)
{
    public async Task InvokeAsync(HttpContext context)
    {
        scopedTracker.CurrentOwner = $"ConstructorCaptured:{context.Request.Query["label"]}";
        await next(context);
    }
}

// FIXED (Exercise 5 Part B): Injecting scoped service into InvokeAsync
public sealed class GoodScopedInvokeMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IRequestAuditTracker scopedTracker)
    {
        scopedTracker.CurrentOwner = $"InvokeScoped:{context.Request.Query["label"]}";
        await next(context);
    }
}

// ============================================================================
// EXERCISE 7: AuditActionFilter + Sample MVC Controller
// ============================================================================
public sealed class AuditActionFilter(ILogger<AuditActionFilter> logger) : IAsyncActionFilter
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = false,
        MaxDepth = 8
    };

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var actionName = context.ActionDescriptor.DisplayName ?? context.ActionDescriptor.Id;
        var correlationId = context.HttpContext.TraceIdentifier;

        var boundArgs = context.ActionArguments.ToDictionary(
            kvp => kvp.Key,
            kvp => SafeSerialize(kvp.Value));

        logger.LogInformation(
            "AuditAction Action={ActionName} CorrelationId={CorrelationId} Arguments={Arguments}",
            actionName,
            correlationId,
            JsonSerializer.Serialize(boundArgs, SerializerOptions));

        await next();
    }

    private static string SafeSerialize(object? value)
    {
        if (value is null) return "null";
        try
        {
            return JsonSerializer.Serialize(value, SerializerOptions);
        }
        catch (Exception ex)
        {
            return $"<unserializable:{value.GetType().Name}:{ex.Message}>";
        }
    }
}

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] CreateOrderRequest request, [FromQuery] bool priority = false)
    {
        return Ok(new
        {
            orderId = Guid.NewGuid(),
            request.Item,
            request.Quantity,
            priority
        });
    }
}

public sealed record CreateOrderRequest(string Item, int Quantity);