using HudayiPortal.Application.Features.YemekMenuleri.Commands.CreateYemekMenu;
using HudayiPortal.Application.Features.YemekMenuleri.Commands.CreateBulkYemekMenu;
using HudayiPortal.Application.Features.YemekMenuleri.Commands.UpdateYemekMenu;
using HudayiPortal.Application.Features.YemekMenuleri.Commands.DeleteYemekMenu;
using HudayiPortal.Application.Features.YemekMenuleri.Queries.ExportAylikYemekMenu;
using HudayiPortal.Application.Features.YemekMenuleri.Queries.GetAylikYemekMenu;
using HudayiPortal.Application.Features.YemekMenuleri.Queries.GetStandartKahvaltiList;
using HudayiPortal.Application.Features.YemekMenuleri.Queries.GetYemekTanimlari;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class YemekMenuController : ControllerBase
{
	private readonly IMediator _mediator;

	public YemekMenuController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateYemekMenuCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id }, id);
	}

	[HttpPost("bulk")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> BulkCreate(
		[FromBody] CreateBulkYemekMenuCommand command,
		CancellationToken cancellationToken)
	{
		var count = await _mediator.Send(command, cancellationToken);
		return Ok(count);
	}

	[HttpGet("aylik")]
	[ProducesResponseType(typeof(List<YemekMenuDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetAylikMenu(
		[FromQuery] int yil,
		[FromQuery] int ay,
		CancellationToken cancellationToken)
	{
		var query = new GetAylikYemekMenuQuery(yil, ay);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	[HttpGet("yemek-tanimlari")]
	[ProducesResponseType(typeof(List<YemekTanimiListItemDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetYemekTanimlari(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetYemekTanimlariListQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpGet("standart-kahvalti")]
	[ProducesResponseType(typeof(List<StandartKahvaltiDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetStandartKahvalti(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetStandartKahvaltiListQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpGet("export-excel")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> ExportAylikMenu(
		[FromQuery] int yil,
		[FromQuery] int ay,
		CancellationToken cancellationToken)
	{
		var query = new ExportAylikYemekMenuQuery(yil, ay);
		var fileBytes = await _mediator.Send(query, cancellationToken);

		return File(
			fileBytes,
			"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
			$"YemekMenu_{yil}_{ay:D2}.xlsx");
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(
		[FromRoute] int id,
		[FromBody] UpdateYemekMenuCommand command,
		CancellationToken cancellationToken)
	{
		if (id != command.Id)
			return BadRequest("Route id ile body id eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteYemekMenuCommand(id), cancellationToken);
		return NoContent();
	}
}
