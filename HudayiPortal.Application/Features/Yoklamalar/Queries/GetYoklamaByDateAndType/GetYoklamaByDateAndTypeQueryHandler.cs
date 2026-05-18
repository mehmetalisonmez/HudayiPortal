using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetYoklamaByDateAndType;

public sealed class GetYoklamaByDateAndTypeQueryHandler
	: IRequestHandler<GetYoklamaByDateAndTypeQuery, List<OgrenciYoklamaDurumDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetYoklamaByDateAndTypeQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<OgrenciYoklamaDurumDto>> Handle(
		GetYoklamaByDateAndTypeQuery request,
		CancellationToken cancellationToken)
	{
		// 1 — Aktif öğrenciler (tüm yurt)
		var ogrenciler = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.RolId == 1 && k.AktifMi == true && k.SilindiMi != true)
			.Include(k => k.Oda)
			.OrderBy(k => k.Oda != null ? k.Oda.OdaNo : "")
			.ThenBy(k => k.Soyad)
			.ThenBy(k => k.Ad)
			.Select(k => new
			{
				k.Id,
				k.Ad,
				k.Soyad,
				OdaNo = k.Oda != null ? k.Oda.OdaNo : null
			})
			.ToListAsync(cancellationToken);

		// 2 — O tarih + tür için mevcut yoklama kayıtları
		var mevcutYoklamalar = await _unitOfWork.Repository<GunlukYoklama>()
			.Where(y => y.Tarih == request.Tarih
						&& y.YoklamaTurId == request.YoklamaTurId
						&& y.SilindiMi != true)
			.ToDictionaryAsync(y => y.KullaniciId, cancellationToken);

		// 3 — LEFT JOIN: öğrenci + mevcut durum
		var result = ogrenciler.Select(o =>
		{
			mevcutYoklamalar.TryGetValue(o.Id, out var yoklama);
			return new OgrenciYoklamaDurumDto(
				o.Id,
				o.Ad,
				o.Soyad,
				o.OdaNo,
				yoklama?.Durum,
				yoklama?.Aciklama
			);
		}).ToList();

		return result;
	}
}
