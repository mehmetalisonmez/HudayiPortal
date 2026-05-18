using HudayiPortal.Application.Features.Odalar.Commands.AssignStudentToRoom;
using HudayiPortal.Application.Features.Odalar.Queries.GetOdaYerlesim;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Personel")]
public class OdaController : ControllerBase
{
	private readonly IMediator _mediator;

	public OdaController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("yerlesim")]
	[ProducesResponseType(typeof(OdaYerlesimResultDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetYerlesim(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetOdaYerlesimQuery(), cancellationToken);
		return Ok(result);
	}

	[HttpPut("ata")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> AssignStudent(
		[FromBody] AssignStudentToRoomCommand command,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}
}
