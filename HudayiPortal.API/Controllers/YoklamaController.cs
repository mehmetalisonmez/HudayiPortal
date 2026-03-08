using HudayiPortal.Application.Features.Yoklamalar.Commands.TakeAttendance;
using HudayiPortal.Application.Features.Yoklamalar.Queries.ExportAylikYoklama;
using HudayiPortal.Application.Features.Yoklamalar.Queries.GetOgrencilerForYoklama;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class YoklamaController : ControllerBase
{
	private readonly IMediator _mediator;

	public YoklamaController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("ogrenciler")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(typeof(List<OgrenciYoklamaDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetOgrencilerForYoklama(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetOgrencilerForYoklamaQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpPost]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> TakeAttendance(
		[FromBody] TakeAttendanceCommand command,
		CancellationToken cancellationToken)
	{
		var count = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(TakeAttendance), new { count }, count);
	}

	[HttpGet("export-excel")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> ExportAylikYoklama(
		[FromQuery] int yil,
		[FromQuery] int ay,
		[FromQuery] int yoklamaTurId,
		CancellationToken cancellationToken)
	{
		var query = new ExportAylikYoklamaQuery(yil, ay, yoklamaTurId);
		var fileBytes = await _mediator.Send(query, cancellationToken);

		return File(
			fileBytes,
			"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
			$"Yoklama_{yil}_{ay:D2}.xlsx");
	}
}
