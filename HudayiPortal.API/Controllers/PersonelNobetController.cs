using HudayiPortal.Application.Features.PersonelNobetleri.Commands.CreatePersonelNobet;
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
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreatePersonelNobetCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id }, id);
	}

	[HttpGet]
	[ProducesResponseType(typeof(List<PersonelNobetDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetPersonelNobetleri(
		[FromQuery] int? personelId,
		[FromQuery] DateTime? baslangicTarihi,
		[FromQuery] DateTime? bitisTarihi,
		CancellationToken cancellationToken)
	{
		var query = new GetPersonelNobetleriQuery(personelId, baslangicTarihi, bitisTarihi);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
}
