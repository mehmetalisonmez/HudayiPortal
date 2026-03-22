using HudayiPortal.Application.Features.MaliIslemler.Commands.CreateMaliIslem;
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

	[HttpGet]
	[ProducesResponseType(typeof(List<MaliIslemDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetMaliIslemler(
		[FromQuery] int? yonId,
		[FromQuery] DateTime? baslangicTarihi,
		[FromQuery] DateTime? bitisTarihi,
		CancellationToken cancellationToken)
	{
		var query = new GetMaliIslemlerQuery(yonId, baslangicTarihi, bitisTarihi);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
}
