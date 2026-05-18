using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SohbetEntity = HudayiPortal.Domain.Entities.Sohbet;

namespace HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetGrubuById;

public sealed class GetSohbetGrubuByIdQueryHandler
	: IRequestHandler<GetSohbetGrubuByIdQuery, SohbetGrubuFullDto>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetSohbetGrubuByIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<SohbetGrubuFullDto> Handle(
		GetSohbetGrubuByIdQuery request,
		CancellationToken cancellationToken)
	{
		var grup = await _unitOfWork.Repository<SohbetGrubu>()
			.Where(g => g.Id == request.Id && g.SilindiMi != true)
			.Select(g => new SohbetGrubuFullDto(
				g.Id,
				g.GrupAdi,
				g.SorumluHocaAdi,
				g.Donem,
				g.OlusturulmaTarihi,
				g.OgrenciSohbetGruplari
					.Where(o => o.SilindiMi != true
						&& o.Kullanici.RolId == 1
						&& o.Kullanici.AktifMi == true
						&& o.Kullanici.SilindiMi != true)
					.OrderBy(o => o.Kullanici.Soyad)
					.ThenBy(o => o.Kullanici.Ad)
					.Select(o => new GrupOgrenciDto(
						o.KullaniciId,
						o.Kullanici.Ad,
						o.Kullanici.Soyad,
						o.Kullanici.Oda != null ? o.Kullanici.Oda.OdaNo : null,
						o.AtanmaTarihi
					))
					.ToList(),
				g.Sohbetler
					.Where(s => s.SilindiMi != true)
					.OrderByDescending(s => s.Tarih)
					.Select(s => new GrupOturumDto(
						s.Id,
						s.Tarih,
						s.KonuBasligi,
						s.SohbetYoklamalari.Count(y => y.SilindiMi != true)
					))
					.ToList()
			))
			.FirstOrDefaultAsync(cancellationToken);

		if (grup is null)
			throw new KeyNotFoundException($"Sohbet grubu bulunamadı: {request.Id}");

		return grup;
	}
}
