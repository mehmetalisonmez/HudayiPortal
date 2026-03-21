using HudayiPortal.Application.Features.YemekYorumlari.Commands.CreateYemekYorum;
using HudayiPortal.Application.Features.YemekYorumlari.Queries.GetYemekYorumlari;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class YemekYorumController : ControllerBase
{
	private readonly IMediator _mediator;

	public YemekYorumController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateYemekYorumCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id }, id);
	}

	[HttpGet("menu/{yemekMenuId}")]
	[ProducesResponseType(typeof(List<YemekYorumDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetByMenuId(
		[FromRoute] int yemekMenuId,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetYemekYorumlariQuery(yemekMenuId), cancellationToken);
		return Ok(result);
	}
}
