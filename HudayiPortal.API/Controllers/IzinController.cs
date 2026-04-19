using HudayiPortal.Application.Features.Izinler.Commands.CreateIzinTalebi;
using HudayiPortal.Application.Features.Izinler.Commands.DeleteIzinTalebi;
using HudayiPortal.Application.Features.Izinler.Commands.UpdateIzinDurumu;
using HudayiPortal.Application.Features.Izinler.Queries.GetIzinTalepleri;
using HudayiPortal.Application.Features.Izinler.Queries.GetIzinTurleri;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IzinController : ControllerBase
{
	private readonly IMediator _mediator;

	public IzinController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("talep")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> CreateTalep(
		[FromBody] CreateIzinTalebiCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(CreateTalep), new { id }, id);
	}

	[HttpPut("onay/{izinId}")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> UpdateDurum(
		[FromRoute] int izinId,
		[FromQuery] int yeniDurum,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new UpdateIzinDurumuCommand(izinId, yeniDurum), cancellationToken);
		if (!result)
		{
			return NotFound();
		}

		return Ok();
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(
		int id,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteIzinTalebiCommand(id), cancellationToken);
		return NoContent();
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<IzinDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetIzinTalepleri(
		[FromQuery] int? kullaniciId,
		[FromQuery] int? onayDurumu,
		CancellationToken cancellationToken)
	{
		var query = new GetIzinTalepleriQuery(kullaniciId, onayDurumu);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	[HttpGet("turler")]
	[ProducesResponseType(typeof(List<IzinTuruDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetIzinTurleri(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetIzinTurleriQuery(), cancellationToken);
		return Ok(result);
	}
}
