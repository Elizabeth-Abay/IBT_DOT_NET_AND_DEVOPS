using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Execise.Middleware;

public sealed class RequestTimingMiddleware(
    RequestDelegate next,
    ILogger<RequestTimingMiddleware> logger)
{
    private const long SlowRequestThresholdMs = 1000;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await next(context);
        }
        finally
        {
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            var level = elapsedMs > SlowRequestThresholdMs
                ? LogLevel.Warning
                : LogLevel.Information;

            logger.Log(
                level,
                "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms [CorrelationId={CorrelationId}]",
                context.Request.Method,
                context.Request.Path.Value ?? "/",
                context.Response.StatusCode,
                elapsedMs,
                context.TraceIdentifier);
        }
    }
}
