using HudayiPortal.Application.Features.PersonelNobetleri.Commands.CreatePersonelNobet;
using HudayiPortal.Application.Features.PersonelNobetleri.Commands.DeletePersonelNobet;
using HudayiPortal.Application.Features.PersonelNobetleri.Commands.UpdatePersonelNobet;
using HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetAvailablePersonel;
using HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetMyNobetler;
using HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetNobetler;
using HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetPersonelNobetleri;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Personel")]
public class PersonelNobetController : ControllerBase
{
	private readonly IMediator _mediator;

	public PersonelNobetController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create(
		[FromBody] CreatePersonelNobetCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(GetNobetler), new { id }, id);
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<PersonelNobetDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetNobetler(
		[FromQuery] DateOnly startDate,
		[FromQuery] DateOnly endDate,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetNobetlerQuery(startDate, endDate), cancellationToken);
		return Ok(result);
	}

	[HttpGet("benim")]
	[ProducesResponseType(typeof(List<PersonelNobetDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetMyNobetler(
		[FromQuery] DateOnly? startDate,
		[FromQuery] DateOnly? endDate,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetMyNobetlerQuery(startDate, endDate), cancellationToken);
		return Ok(result);
	}

	[HttpGet("personeller")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(List<AvailablePersonelDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAvailablePersonel(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetAvailablePersonelQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpPut("{id:int}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(
		int id,
		[FromBody] UpdatePersonelNobetCommand command,
		CancellationToken cancellationToken)
	{
		if (id != command.Id)
			return BadRequest("Route id ile body id eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	[HttpDelete("{id:int}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeletePersonelNobetCommand(id), cancellationToken);
		return NoContent();
	}
}
