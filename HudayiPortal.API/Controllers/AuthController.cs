using HudayiPortal.Application.Features.Auth.Commands.Register;
using HudayiPortal.Application.Features.Auth.Commands.VerifyEmail;
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

	[HttpPost("register")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Register(
		[FromBody] RegisterCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Register), new { id }, id);
	}

	[HttpPost("login")]
	[ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Login(
		[FromBody] LoginQuery query,
		CancellationToken cancellationToken)
	{
		var response = await _mediator.Send(query, cancellationToken);
		return Ok(response);
	}

	[HttpGet("verify-email")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> VerifyEmail(
		[FromQuery] string token,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(new VerifyEmailCommand(token), cancellationToken);
		return Ok("Email successfully verified.");
	}
}