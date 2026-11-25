using  AddisEats.Api.Services.Restaurant;
using  AddisEats.Api.Services.MenuItems;
using  AddisEats.Api.Service.Order;
using AddisEats.Api.Services;
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



// this is used for service registry and building ur app

// in the service u add the controller
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var traceId = System.Diagnostics.Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
            var problem = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred.",
                Instance = context.HttpContext.Request.Path
            };
            problem.Extensions["code"] = "VALIDATION.FAILED";
            problem.Extensions["traceId"] = traceId;

            return new BadRequestObjectResult(problem)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    }); ;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// in the dependency injection the controller receives an object of this interface
// register this interface as this service
builder.Services.AddScoped< RestaurantServiceInterface , RestaurantService >();
builder.Services.AddScoped< MenuItemsServiceInterface , MenuItemService>();
builder.Services.AddScoped<OrderInterface , OrderService>();
builder.Services.AddSingleton<IClock, SystemClock>();



// service registration before build using Add
// middleware - after build using use



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseMiddleware<RequestTimingMiddleware>();

app.UseMiddleware<ApiKeyMiddleware>();



app.UseHttpsRedirection();

app.MapControllers();

app.Run();

