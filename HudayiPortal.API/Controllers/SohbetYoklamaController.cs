using HudayiPortal.Application.Features.SohbetYoklamalar.Commands.TakeSohbetAttendance;
using HudayiPortal.Application.Features.SohbetYoklamalar.Queries.ExportSohbetYoklamaToExcel;
using HudayiPortal.Application.Features.SohbetYoklamalar.Queries.GetSohbetYoklama;
using HudayiPortal.Application.Features.Yoklamalar.Queries.GetSohbetGruplari;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SohbetYoklamaController : ControllerBase
{
	private readonly IMediator _mediator;

	public SohbetYoklamaController(IMediator mediator)
	{
		_mediator = mediator;
	}

	/// <summary>Sohbet gruplarını listeler</summary>
	[HttpGet("gruplar")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(typeof(List<SohbetGrubuDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetSohbetGruplari(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetSohbetGruplariQuery(), cancellationToken);
		return Ok(result);
	}

	/// <summary>Tarih + GrupId'ye göre sohbet yoklamasını getirir (oturum yoksa oluşturur)</summary>
	[HttpGet("yoklama")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(typeof(SohbetYoklamaResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetSohbetYoklama(
		[FromQuery] DateOnly tarih,
		[FromQuery] int grupId,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetSohbetYoklamaQuery(tarih, grupId), cancellationToken);
		return Ok(result);
	}

	/// <summary>Sohbet yoklaması kaydet (UPSERT)</summary>
	[HttpPost("yoklama")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> TakeSohbetAttendance(
		[FromBody] TakeSohbetAttendanceCommand command,
		CancellationToken cancellationToken)
	{
		var count = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(TakeSohbetAttendance), new { count }, count);
	}

	/// <summary>Sohbet yoklama pivot Excel export</summary>
	[HttpGet("export/sohbet")]
	[Authorize(Roles = "Admin,Personel")]
	public async Task<IActionResult> ExportSohbetYoklama(
		[FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate,
		[FromQuery] int grupId, CancellationToken cancellationToken)
	{
		var query = new ExportSohbetYoklamaToExcelQuery(startDate, endDate, grupId);
		var fileBytes = await _mediator.Send(query, cancellationToken);
		var fileName = $"SohbetYoklama_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.xlsx";
		return File(fileBytes,
			"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
			fileName);
	}
}
