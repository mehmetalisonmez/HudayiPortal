using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Wrappers;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace HudayiPortal.API.Middlewares;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		httpContext.Response.ContentType = "application/json";

		ErrorResult errorResult;

		if (exception is HudayiPortal.Application.Exceptions.ValidationException validationException)
		{
			httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
			errorResult = new ErrorResult
			{
				Message = validationException.Message,
				Errors = validationException.Errors
			};
		}
		else if (exception is FluentValidation.ValidationException fluentValidationException)
		{
			httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
			errorResult = new ErrorResult
			{
				Message = "Doğrulama hatası oluştu.",
				Errors = fluentValidationException.Errors.Select(e => e.ErrorMessage).ToList()
			};
		}
		else if (exception is BusinessException businessException)
		{
			httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
			errorResult = new ErrorResult
			{
				Message = businessException.Message,
				Errors = new List<string> { businessException.Message }
			};
		}
		else if (exception is UnauthorizedAccessException unauthorizedAccessException)
		{
			httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
			errorResult = new ErrorResult
			{
				Message = unauthorizedAccessException.Message,
				Errors = new List<string> { unauthorizedAccessException.Message }
			};
		}
		else
		{
			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			errorResult = new ErrorResult
			{
				Message = "Sunucu tarafında beklenmeyen bir hata oluştu.",
				Errors = new List<string> { exception.Message }
			};
		}

		var options = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		var jsonResponse = JsonSerializer.Serialize(errorResult, options);
		await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

		return true;
	}
}