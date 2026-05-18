using HudayiPortal.Application.Features.Etkinlikler.Commands.AddEtkinlikYorum;
using HudayiPortal.Application.Features.Etkinlikler.Commands.CreateEtkinlik;
using HudayiPortal.Application.Features.Etkinlikler.Commands.DeleteEtkinlik;
using HudayiPortal.Application.Features.Etkinlikler.Commands.JoinEtkinlik;
using HudayiPortal.Application.Features.Etkinlikler.Commands.LeaveEtkinlik;
using HudayiPortal.Application.Features.Etkinlikler.Commands.ToggleLike;
using HudayiPortal.Application.Features.Etkinlikler.Commands.UpdateEtkinlik;
using HudayiPortal.Application.Features.Etkinlikler.Commands.UpdateKatilimDurumu;
using HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikDetay;
using HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikKatilimcilari;
using HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EtkinlikController : ControllerBase
{
	private readonly IMediator _mediator;

	public EtkinlikController(IMediator mediator)
	{
		_mediator = mediator;
	}

	// ─── Yönetim Komutları (Admin / Personel) ────────────────────────────────

	/// <summary>Yeni etkinlik oluşturur.</summary>
	[HttpPost]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create(
		[FromBody] CreateEtkinlikCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(GetDetay), new { id }, id);
	}

	/// <summary>Mevcut etkinliği günceller.</summary>
	[HttpPut("{id:int}")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(
		[FromRoute] int id,
		[FromBody] UpdateEtkinlikCommand command,
		CancellationToken cancellationToken)
	{
		if (id != command.Id)
			return BadRequest("Route id ile body id eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	/// <summary>Etkinliği soft-delete ile siler.</summary>
	[HttpDelete("{id:int}")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(new DeleteEtkinlikCommand(id), cancellationToken);
		return NoContent();
	}

	/// <summary>Bir etkinliğin katılımcı listesini getirir.</summary>
	[HttpGet("{id:int}/katilimcilar")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(typeof(List<KatilimciDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetKatilimcilar(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetEtkinlikKatilimcilariQuery(id), cancellationToken);
		return Ok(result);
	}

	/// <summary>Katılımcının katılım durumunu (Katıldı/Katılmadı/Bekleniyor) günceller.</summary>
	[HttpPut("katilimci/{katilimciId:int}/durum")]
	[Authorize(Roles = "Admin,Personel")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateKatilimDurumu(
		[FromRoute] int katilimciId,
		[FromBody] UpdateKatilimDurumuCommand command,
		CancellationToken cancellationToken)
	{
		if (katilimciId != command.KatilimciId)
			return BadRequest("Route id ile body id eşleşmiyor.");

		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	// ─── Sorgular (Tüm Roller) ───────────────────────────────────────────────

	/// <summary>
	/// Etkinlikleri filtreli listeler.
	/// aktif: true=aktif, false=geçmiş, null=tümü
	/// ucretsiz: true=yalnızca ücretsiz, false=yalnızca ücretli, null=tümü
	/// </summary>
	[HttpGet]
	[ProducesResponseType(typeof(List<EtkinlikListDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetEtkinlikler(
		[FromQuery] bool? aktif,
		[FromQuery] bool? ucretsiz,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetEtkinliklerQuery(aktif, ucretsiz), cancellationToken);
		return Ok(result);
	}

	/// <summary>Etkinlik detayını yorumlar, beğeni sayısı ve kullanıcı durumu ile getirir.</summary>
	[HttpGet("{id:int}")]
	[ProducesResponseType(typeof(EtkinlikDetayDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetDetay(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		var result = await _mediator.Send(new GetEtkinlikDetayQuery(id), cancellationToken);
		return Ok(result);
	}

	// ─── Sosyal Komutlar (Tüm Roller) ───────────────────────────────────────

	/// <summary>Giriş yapan kullanıcıyı etkinliğe kaydeder.</summary>
	[HttpPost("katil")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Katil(
		[FromBody] JoinEtkinlikCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Katil), new { id }, id);
	}

	/// <summary>Giriş yapan kullanıcının etkinlik kaydını soft-delete ile iptal eder.</summary>
	[HttpPost("ayril")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Ayril(
		[FromBody] LeaveEtkinlikCommand command,
		CancellationToken cancellationToken)
	{
		await _mediator.Send(command, cancellationToken);
		return NoContent();
	}

	/// <summary>Etkinliğe yorum ekler.</summary>
	[HttpPost("yorum")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Yorum(
		[FromBody] AddEtkinlikYorumCommand command,
		CancellationToken cancellationToken)
	{
		var id = await _mediator.Send(command, cancellationToken);
		return CreatedAtAction(nameof(Yorum), new { id }, id);
	}

	/// <summary>Etkinliği beğen veya beğeniden vazgeç. true=beğenildi, false=kaldırıldı.</summary>
	[HttpPost("like/{etkinlikId:int}")]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	public async Task<IActionResult> ToggleLike(
		[FromRoute] int etkinlikId,
		CancellationToken cancellationToken)
	{
		var isLiked = await _mediator.Send(new ToggleLikeCommand(etkinlikId), cancellationToken);
		return Ok(new { isLiked });
	}
}
