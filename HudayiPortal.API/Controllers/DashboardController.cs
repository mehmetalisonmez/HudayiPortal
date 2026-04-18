using HudayiPortal.Application.Features.Dashboard.Queries.GetOgrenciDashboard;
using HudayiPortal.Application.Features.Dashboard.Queries.GetYoneticiDashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
	private readonly IMediator _mediator;

	public DashboardController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("yonetici")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(typeof(YoneticiDashboardDto), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetYoneticiDashboard(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetYoneticiDashboardQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpGet("ogrenci")]
	[Authorize(Roles = "Öðrenci")]
	[ProducesResponseType(typeof(OgrenciDashboardDto), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetOgrenciDashboard(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetOgrenciDashboardQuery(), cancellationToken);
		return Ok(result);
	}
}