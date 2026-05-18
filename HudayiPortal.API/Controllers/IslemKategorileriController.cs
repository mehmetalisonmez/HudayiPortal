using HudayiPortal.Application.Features.IslemKategorileri.Commands.CreateIslemKategorisi;
using HudayiPortal.Application.Features.IslemKategorileri.Commands.DeleteIslemKategorisi;
using HudayiPortal.Application.Features.IslemKategorileri.Commands.UpdateIslemKategorisi;
using HudayiPortal.Application.Features.IslemKategorileri.Queries.GetIslemKategorileri;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Personel")]
public class IslemKategorileriController : ControllerBase
{
	private readonly IMediator _mediator;

	public IslemKategorileriController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<IslemKategorisiDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetAll(
		[FromQuery] int? yonId,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetIslemKategorileriQuery(yonId), cancellationToken);
		return Ok(result);
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateIslemKategorisiCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(GetAll), new { id }, id);
	}

	[HttpPut("{id:int}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Update(
		int id,
		[FromBody] UpdateIslemKategorisiCommand command,
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
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Delete(
		int id,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteIslemKategorisiCommand(id), cancellationToken);
		return NoContent();
	}
}
