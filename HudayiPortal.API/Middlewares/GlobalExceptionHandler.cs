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

		if (exception is ValidationException validationException)
		{
			httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
			errorResult = new ErrorResult
			{
				Message = validationException.Message,
				Errors = validationException.Errors
			};
		}
		else
		{
			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			errorResult = new ErrorResult
			{
				Message = "Sunucu tarafýnda beklenmeyen bir hata oluþtu.",
				Errors = new List<string>()
			};
		}

		var jsonResponse = JsonSerializer.Serialize(errorResult);
		await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

		return true;
	}
}