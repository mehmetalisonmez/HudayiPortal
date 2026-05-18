using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikler;

public sealed class GetEtkinliklerQueryHandler : IRequestHandler<GetEtkinliklerQuery, List<EtkinlikListDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public GetEtkinliklerQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<List<EtkinlikListDto>> Handle(GetEtkinliklerQuery request, CancellationToken cancellationToken)
	{
		var kullaniciId = _currentUserService.UserId;
		var now = DateTime.UtcNow;

		var query = _unitOfWork.Repository<Etkinlik>()
			.Where(e => e.SilindiMi != true);

		// Aktif filtresi
		if (request.Aktif.HasValue)
		{
			query = request.Aktif.Value
				? query.Where(e => e.BitisTarihi == null || e.BitisTarihi >= now)
				: query.Where(e => e.BitisTarihi != null && e.BitisTarihi < now);
		}

		// Ücret filtresi
		if (request.Ucretsiz.HasValue)
		{
			query = request.Ucretsiz.Value
				? query.Where(e => e.Ucret == null || e.Ucret == 0)
				: query.Where(e => e.Ucret != null && e.Ucret > 0);
		}

		var result = await query
			.OrderBy(e => e.BaslangicTarihi)
			.Select(e => new EtkinlikListDto(
				e.Id,
				e.Baslik,
				e.Aciklama,
				e.BaslangicTarihi,
				e.BitisTarihi,
				e.SonKayitTarihi,
				e.Ucret,
				e.ZorunluMu,
				e.ResimUrl,
				e.Begeniler.Count,
				e.EtkinlikYorumlari.Count(y => y.SilindiMi != true),
				e.EtkinlikKatilimcilari.Count(k => k.SilindiMi != true),
				e.Begeniler.Any(b => b.KullaniciId == kullaniciId),
				e.EtkinlikKatilimcilari.Any(k => k.KullaniciId == kullaniciId && k.SilindiMi != true)
			))
			.ToListAsync(cancellationToken);

		return result;
	}
}
