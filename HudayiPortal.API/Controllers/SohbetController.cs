using HudayiPortal.Application.Features.Sohbet.Commands.AssignOgrenci;
using HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetGrubu;
using HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetSession;
using HudayiPortal.Application.Features.Sohbet.Commands.DeleteSohbetGrubu;
using HudayiPortal.Application.Features.Sohbet.Commands.DeleteSohbetSession;
using HudayiPortal.Application.Features.Sohbet.Commands.SyncOgrenciler;
using HudayiPortal.Application.Features.Sohbet.Commands.UpdateSohbetGrubu;
using HudayiPortal.Application.Features.Sohbet.Commands.UpdateSohbetSession;
using HudayiPortal.Application.Features.Sohbet.Queries.GetAllSohbetGruplari;
using HudayiPortal.Application.Features.Sohbet.Queries.GetAvailableOgrenciler;
using HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetGrubuById;
using HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetSessionsByGrup;
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

	// ── Grup CRUD ────────────────────────────

	[HttpGet("gruplar")]
	[ProducesResponseType(typeof(List<SohbetGrubuDetailDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetGruplar(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetAllSohbetGruplariQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpGet("gruplar/{id}")]
	[ProducesResponseType(typeof(SohbetGrubuFullDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetGrupById(int id, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetSohbetGrubuByIdQuery(id), cancellationToken);
		return Ok(result);
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

	[HttpPut("gruplar/{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateGrup(
		int id,
		[FromBody] UpdateSohbetGrubuCommand command,
		CancellationToken cancellationToken)
	{
		if (id != command.Id)
			return BadRequest("Route id ile body id eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	[HttpDelete("gruplar/{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteGrup(int id, CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteSohbetGrubuCommand(id), cancellationToken);
		return NoContent();
	}

	// ── Öğrenci Atama ────────────────────────

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

	[HttpPut("gruplar/{id}/ogrenciler")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> SyncOgrenciler(
		int id,
		[FromBody] SyncOgrencilerCommand command,
		CancellationToken cancellationToken)
	{
		if (id != command.SohbetGrupId)
			return BadRequest("Route id ile body SohbetGrupId eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	[HttpGet("gruplar/{id}/ogrenciler/available")]
	[ProducesResponseType(typeof(List<AvailableOgrenciDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAvailableOgrenciler(int id, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetAvailableOgrencilerQuery(id), cancellationToken);
		return Ok(result);
	}

	// ── Oturum CRUD ──────────────────────────

	[HttpGet("gruplar/{id}/oturumlar")]
	[ProducesResponseType(typeof(List<GrupOturumDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetOturumlar(int id, CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetSohbetSessionsByGrupQuery(id), cancellationToken);
		return Ok(result);
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

	[HttpPut("oturum/{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateOturum(
		int id,
		[FromBody] UpdateSohbetSessionCommand command,
		CancellationToken cancellationToken)
	{
		if (id != command.Id)
			return BadRequest("Route id ile body id eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	[HttpDelete("oturum/{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteOturum(int id, CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteSohbetSessionCommand(id), cancellationToken);
		return NoContent();
	}
}
