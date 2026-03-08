using HudayiPortal.Application.Features.YemekMenuleri.Commands.CreateYemekMenu;
using HudayiPortal.Application.Features.YemekMenuleri.Queries.ExportAylikYemekMenu;
using HudayiPortal.Application.Features.YemekMenuleri.Queries.GetAylikYemekMenu;
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
}
