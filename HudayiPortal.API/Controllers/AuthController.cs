using HudayiPortal.Application.Features.Auth.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;

	public AuthController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("login")]
	[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Login(
		[FromBody] LoginQuery query,
		CancellationToken cancellationToken)
	{
		var response = await _mediator.Send(query, cancellationToken);
		return Ok(response);
	}
}