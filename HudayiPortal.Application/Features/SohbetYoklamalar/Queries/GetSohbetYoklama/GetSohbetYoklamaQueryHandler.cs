using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SohbetEntity = HudayiPortal.Domain.Entities.Sohbet;

namespace HudayiPortal.Application.Features.SohbetYoklamalar.Queries.GetSohbetYoklama;

public sealed class GetSohbetYoklamaQueryHandler
	: IRequestHandler<GetSohbetYoklamaQuery, SohbetYoklamaResponse>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetSohbetYoklamaQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<SohbetYoklamaResponse> Handle(
		GetSohbetYoklamaQuery request,
		CancellationToken cancellationToken)
	{
		// 1 — Bugüne ait Sohbet oturumunu bul veya oluştur
		var tarihStart = request.Tarih.ToDateTime(TimeOnly.MinValue);
		var tarihEnd = request.Tarih.ToDateTime(TimeOnly.MaxValue);

		var sohbet = await _unitOfWork.Repository<SohbetEntity>()
			.Where(s => s.SohbetGrupId == request.GrupId
						&& s.Tarih >= tarihStart && s.Tarih <= tarihEnd
						&& s.SilindiMi != true)
			.FirstOrDefaultAsync(cancellationToken);

		if (sohbet == null)
		{
			sohbet = new SohbetEntity
			{
				SohbetGrupId = request.GrupId,
				Tarih = tarihStart,
				OlusturulmaTarihi = DateTime.UtcNow,
				SilindiMi = false
			};
			await _unitOfWork.Repository<SohbetEntity>().AddAsync(sohbet, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);
		}

		// 2 — Bu gruba kayıtlı aktif öğrencileri getir
		var ogrenciler = await _unitOfWork.Repository<OgrenciSohbetGrubu>()
			.Where(og => og.SohbetGrupId == request.GrupId && og.SilindiMi != true)
			.Where(og => og.Kullanici.RolId == 1
						 && og.Kullanici.AktifMi == true
						 && og.Kullanici.SilindiMi != true)
			.Select(og => new
			{
				og.Kullanici.Id,
				og.Kullanici.Ad,
				og.Kullanici.Soyad,
				OdaNo = og.Kullanici.Oda != null ? og.Kullanici.Oda.OdaNo : null
			})
			.OrderBy(o => o.OdaNo ?? "")
			.ThenBy(o => o.Soyad)
			.ThenBy(o => o.Ad)
			.ToListAsync(cancellationToken);

		// 3 — Bu sohbet oturumuna ait mevcut yoklama kayıtları
		var mevcutYoklamalar = await _unitOfWork.Repository<SohbetYoklama>()
			.Where(sy => sy.SohbetId == sohbet.Id && sy.SilindiMi != true)
			.ToDictionaryAsync(sy => sy.KullaniciId, cancellationToken);

		// 4 — LEFT JOIN: öğrenci + mevcut katılım durumu
		var result = ogrenciler.Select(o =>
		{
			mevcutYoklamalar.TryGetValue(o.Id, out var yoklama);
			return new SohbetOgrenciDurumDto(
				o.Id,
				o.Ad,
				o.Soyad,
				o.OdaNo,
				yoklama?.KatilimDurumu,
				yoklama?.MazeretAciklamasi
			);
		}).ToList();

		return new SohbetYoklamaResponse(sohbet.Id, result);
	}
}
