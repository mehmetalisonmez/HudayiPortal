using HudayiPortal.Application.Features.Roller.Queries.GetRolList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RolController : ControllerBase
{
	private readonly IMediator _mediator;

	public RolController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(typeof(List<RolDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetRolListQuery(), cancellationToken);
		return Ok(result);
	}
}
