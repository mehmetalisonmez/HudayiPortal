using HudayiPortal.Application.Features.Sikayetler.Commands.CreateSikayet;
using HudayiPortal.Application.Features.Sikayetler.Commands.UpdateSikayetCevap;
using HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SikayetController : ControllerBase
{
	private readonly IMediator _mediator;

	public SikayetController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateSikayetCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id }, id);
	}

	[HttpPut("cevap")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> UpdateCevap(
		[FromBody] UpdateSikayetCevapCommand command,
		CancellationToken cancellationToken)
	{
		var updated = await _mediator.Send(command, cancellationToken);
		if (!updated)
		{
			return NotFound();
		}

		return Ok();
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<SikayetDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetSikayetler(
		[FromQuery] int? gonderenKullaniciId,
		[FromQuery] int? durum,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetSikayetlerQuery(gonderenKullaniciId, durum), cancellationToken);
		return Ok(result);
	}
}
