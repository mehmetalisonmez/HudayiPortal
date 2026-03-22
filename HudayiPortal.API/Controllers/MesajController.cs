using HudayiPortal.Application.Features.Mesajlar.Queries.GetMesajlar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MesajController : ControllerBase
{
	private readonly IMediator _mediator;

	public MesajController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<MesajDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetMesajlar(
		[FromQuery] int? aliciId,
		[FromQuery] int? chatGrupId,
		CancellationToken cancellationToken)
	{
		var query = new GetMesajlarQuery(aliciId, chatGrupId);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
}
