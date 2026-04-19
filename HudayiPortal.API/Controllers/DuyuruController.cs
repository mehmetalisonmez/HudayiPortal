using HudayiPortal.Application.Features.Duyurular.Commands.CreateDuyuru;
using HudayiPortal.Application.Features.Duyurular.Commands.UpdateDuyuru;
using HudayiPortal.Application.Features.Duyurular.Commands.DeleteDuyuru;
using HudayiPortal.Application.Features.Duyurular.Queries.GetAktifDuyurular;
using HudayiPortal.Application.Features.Duyurular.Queries.GetAllDuyurular;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DuyuruController : ControllerBase
{
	private readonly IMediator _mediator;

	public DuyuruController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateDuyuruCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id }, id);
	}

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(
		int id,
		[FromBody] UpdateDuyuruCommand command,
		CancellationToken cancellationToken)
	{
		// Route'daki id ile body'deki id eşleşmeli
		if (id != command.Id)
			return BadRequest("Route id ile body id eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(
		int id,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteDuyuruCommand(id), cancellationToken);
		return NoContent();
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<DuyuruDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetAllDuyurularQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpGet("aktif")]
	[ProducesResponseType(typeof(List<DuyuruDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetAktifDuyurular(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetAktifDuyurularQuery(), cancellationToken);
		return Ok(result);
	}
}
