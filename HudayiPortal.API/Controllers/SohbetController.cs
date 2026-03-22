using HudayiPortal.Application.Features.Sohbet.Commands.AssignOgrenci;
using HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetGrubu;
using HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetSession;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Personel")]
public class SohbetController : ControllerBase
{
	private readonly IMediator _mediator;

	public SohbetController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("grup")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> CreateGrup(
		[FromBody] CreateSohbetGrubuCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(CreateGrup), new { id }, id);
	}

	[HttpPost("ata")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> AssignOgrenci(
		[FromBody] AssignOgrenciToSohbetGrubuCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(AssignOgrenci), new { id }, id);
	}

	[HttpPost("oturum")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> CreateOturum(
		[FromBody] CreateSohbetSessionCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(CreateOturum), new { id }, id);
	}
}
