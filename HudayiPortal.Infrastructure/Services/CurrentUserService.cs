using HudayiPortal.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HudayiPortal.Infrastructure.Services;

public sealed class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public int UserId
	{
		get
		{
			var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			if (int.TryParse(userIdClaim, out var userId))
			{
				return userId;
			}

			return 0;
		}
	}
}
