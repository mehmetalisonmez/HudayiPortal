using HudayiPortal.Application.Features.YemekTanimlari.Commands.CreateYemekTanimi;
using HudayiPortal.Application.Features.YemekTanimlari.Commands.UpdateYemekTanimi;
using HudayiPortal.Application.Features.YemekTanimlari.Commands.DeleteYemekTanimi;
using HudayiPortal.Application.Features.YemekTanimlari.Queries.GetAllYemekTanimlari;
using HudayiPortal.Application.Features.YemekTanimlari.Queries.GetYemekKategorileri;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Personel")]
public class YemekTanimiController : ControllerBase
{
	private readonly IMediator _mediator;

	public YemekTanimiController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<YemekTanimiDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetAllYemekTanimlariQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpGet("kategoriler")]
	[ProducesResponseType(typeof(List<YemekKategorisiDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetKategoriler(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetYemekKategorileriListQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateYemekTanimiCommand command,
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
		[FromRoute] int id,
		[FromBody] UpdateYemekTanimiCommand command,
		CancellationToken cancellationToken)
	{
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
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteYemekTanimiCommand(id), cancellationToken);
		return NoContent();
	}
}
