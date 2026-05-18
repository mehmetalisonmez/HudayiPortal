using HudayiPortal.Application.Features.MaliIslemler.Commands.CreateMaliIslem;
using HudayiPortal.Application.Features.MaliIslemler.Commands.DeleteMaliIslem;
using HudayiPortal.Application.Features.MaliIslemler.Commands.UpdateMaliIslem;
using HudayiPortal.Application.Features.MaliIslemler.Queries.GetFinansDashboard;
using HudayiPortal.Application.Features.MaliIslemler.Queries.GetMaliIslemler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Personel")]
public class MaliIslemController : ControllerBase
{
	private readonly IMediator _mediator;

	public MaliIslemController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateMaliIslemCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id }, id);
	}

	[HttpPut("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Update(
		int id,
		[FromBody] UpdateMaliIslemCommand command,
		CancellationToken cancellationToken)
	{
		if (id != command.Id)
			return BadRequest("Route id ile body id eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Delete(
		int id,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteMaliIslemCommand(id), cancellationToken);
		return NoContent();
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<MaliIslemDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetMaliIslemler(
		[FromQuery] int? yonId,
		[FromQuery] DateTime? baslangicTarihi,
		[FromQuery] DateTime? bitisTarihi,
		[FromQuery] int? kategoriId,
		CancellationToken cancellationToken)
	{
		var query = new GetMaliIslemlerQuery(yonId, baslangicTarihi, bitisTarihi, kategoriId);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	[HttpGet("dashboard")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(FinansDashboardDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetDashboard(
		[FromQuery] DateTime? baslangicTarihi,
		[FromQuery] DateTime? bitisTarihi,
		CancellationToken cancellationToken)
	{
		var query = new GetFinansDashboardQuery(baslangicTarihi, bitisTarihi);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
}
