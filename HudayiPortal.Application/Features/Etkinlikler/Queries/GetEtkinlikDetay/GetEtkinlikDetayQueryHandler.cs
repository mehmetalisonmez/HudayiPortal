using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikDetay;

public sealed class GetEtkinlikDetayQueryHandler : IRequestHandler<GetEtkinlikDetayQuery, EtkinlikDetayDto>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public GetEtkinlikDetayQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<EtkinlikDetayDto> Handle(GetEtkinlikDetayQuery request, CancellationToken cancellationToken)
	{
		var kullaniciId = _currentUserService.UserId;

		var etkinlik = await _unitOfWork.Repository<Etkinlik>()
			.Where(e => e.Id == request.Id && e.SilindiMi != true)
			.Select(e => new EtkinlikDetayDto(
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
				e.EtkinlikKatilimcilari.Any(k => k.KullaniciId == kullaniciId && k.SilindiMi != true),
				e.EtkinlikYorumlari
					.Where(y => y.SilindiMi != true)
					.OrderBy(y => y.OlusturulmaTarihi)
					.Select(y => new YorumDto(
						y.Id,
						y.YorumMetni,
						y.OlusturulmaTarihi,
						y.Kullanici.Ad + " " + y.Kullanici.Soyad
					))
					.ToList()
			))
			.FirstOrDefaultAsync(cancellationToken);

		if (etkinlik is null)
			throw new ValidationException("Etkinlik bulunamadı.", new List<string> { $"Id={request.Id} olan etkinlik mevcut değil." });

		return etkinlik;
	}
}
