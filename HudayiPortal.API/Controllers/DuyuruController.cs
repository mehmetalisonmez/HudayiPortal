using HudayiPortal.Application.Features.Duyurular.Commands.CreateDuyuru;
using HudayiPortal.Application.Features.Duyurular.Queries.GetAktifDuyurular;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DuyuruController : ControllerBase
{
	private readonly IMediator _mediator;

	public DuyuruController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateDuyuruCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id }, id);
	}

	[HttpGet("aktif")]
	[ProducesResponseType(typeof(List<DuyuruDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetAktifDuyurular(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetAktifDuyurularQuery(), cancellationToken);
		return Ok(result);
	}
}
