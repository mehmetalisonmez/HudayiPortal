using HudayiPortal.Application.Features.Kullanicilar.Commands.CreateKullanici;
using HudayiPortal.Application.Features.Kullanicilar.Commands.UpdateKullanici;
using HudayiPortal.Application.Features.Kullanicilar.Commands.DeleteKullanici;
using HudayiPortal.Application.Features.Kullanicilar.Queries.GetOgrenciList;
using HudayiPortal.Application.Features.Kullanicilar.Queries.GetOdaList;
using HudayiPortal.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class KullaniciController : ControllerBase
{
	private readonly IMediator _mediator;

	public KullaniciController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("ogrenciler")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(typeof(PagedResponse<KullaniciListDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetOgrenciList(
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 10,
		[FromQuery] string? searchTerm = null,
		CancellationToken cancellationToken = default)
	{
		var query = new GetOgrenciListQuery(pageNumber, pageSize, searchTerm);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	[HttpPost]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Create(
		[FromBody] CreateKullaniciCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id }, id);
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(
		int id,
		[FromBody] UpdateKullaniciCommand command,
		CancellationToken cancellationToken)
	{
		// Route'daki id ile body'deki id eşleşmeli
		if (id != command.Id)
			return BadRequest("Route id ile body id eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(
		int id,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteKullaniciCommand(id), cancellationToken);
		return NoContent();
	}

	[HttpGet("odalar")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(typeof(List<OdaListDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> GetOdaList(CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetOdaListQuery(), cancellationToken);
		return Ok(result);
	}
}