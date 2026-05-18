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
			var claim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			return int.TryParse(claim, out var id) ? id : 0;
		}
	}

	public string Role =>
		_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

	public int RoleId
	{
		get
		{
			var claim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("roleId");
			return int.TryParse(claim, out var id) ? id : 0;
		}
	}
}
