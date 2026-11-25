using System.Net;
namespace Execise.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;

public sealed class GlobalExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<GlobalExceptionMiddleware> _logger;
	private readonly IHostEnvironment _env;

	public GlobalExceptionMiddleware(
		RequestDelegate next,
		ILogger<GlobalExceptionMiddleware> logger,
		IHostEnvironment env)
	{
		_next = next;
		_logger = logger;
		_env = env;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			var correlationId = context.TraceIdentifier;

			_logger.LogError(
				ex,
				"Unhandled exception on {Method} {Path} [CorrelationId={CorrelationId}]",
				context.Request.Method,
				context.Request.Path.Value ?? "/",
				correlationId);

			if (context.Response.HasStarted)
			{
				_logger.LogWarning(
					"Response has already started; cannot write error payload [CorrelationId={CorrelationId}]",
					correlationId);
				throw;
			}

			var statusCode = ex switch
			{
				ArgumentException or BadHttpRequestException => StatusCodes.Status400BadRequest,
				UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
				KeyNotFoundException => StatusCodes.Status404NotFound,
				_ => StatusCodes.Status500InternalServerError
			};

			var problem = new ProblemDetails
			{
				Status = statusCode,
				Title = statusCode == StatusCodes.Status500InternalServerError
					? "An unexpected error occurred."
					: "Request failed.",
				Detail = _env.IsDevelopment()
					? ex.Message
					: (statusCode == StatusCodes.Status500InternalServerError
						? "An internal server error occurred while processing your request."
						: ex.Message),
				Instance = context.Request.Path
			};

			problem.Extensions["correlationId"] = correlationId;
			problem.Extensions["traceId"] = correlationId;

			if (_env.IsDevelopment())
			{
				problem.Extensions["exceptionType"] = ex.GetType().FullName;
			}

			context.Response.Clear();
			context.Response.StatusCode = statusCode;
			context.Response.ContentType = "application/problem+json";
			context.Response.Headers["X-Correlation-Id"] = correlationId;

			await context.Response.WriteAsJsonAsync(problem);
		}
	}
}