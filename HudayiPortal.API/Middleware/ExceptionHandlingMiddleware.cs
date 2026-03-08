using FluentValidation;
using System.Net;
using System.Text.Json;

namespace HudayiPortal.API.Middleware;

public sealed class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionHandlingMiddleware> _logger;

	public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (ValidationException ex)
		{
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			context.Response.ContentType = "application/json";

			var errors = ex.Errors
				.GroupBy(e => e.PropertyName)
				.ToDictionary(
					g => g.Key,
					g => g.Select(e => e.ErrorMessage).ToArray());

			var response = new
			{
				Title = "Doðrulama hatasý oluþtu.",
				Status = 400,
				Errors = errors
			};

			await context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
		catch (UnauthorizedAccessException ex)
		{
			context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
			context.Response.ContentType = "application/json";

			var response = new
			{
				Title = ex.Message,
				Status = 401
			};

			await context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Beklenmeyen bir hata oluþtu.");

			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";

			var response = new
			{
				Title = "Sunucu hatasý oluþtu.",
				Status = 500
			};

			await context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
	}
}