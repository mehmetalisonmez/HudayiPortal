using HudayiPortal.Application.Features.Etkinlikler.Commands.CreateEtkinlik;
using HudayiPortal.Application.Features.Etkinlikler.Commands.JoinEtkinlik;
using HudayiPortal.Application.Features.Etkinlikler.Queries.GetAktifEtkinlikler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EtkinlikController : ControllerBase
{
	private readonly IMediator _mediator;

	public EtkinlikController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateEtkinlikCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id }, id);
	}

	[HttpPost("katil")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Katil(
		[FromBody] JoinEtkinlikCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Katil), new { id }, id);
	}

	[HttpGet("aktif")]
	[ProducesResponseType(typeof(List<EtkinlikDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetAktif(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetAktifEtkinliklerQuery(), cancellationToken);
		return Ok(result);
	}
}
