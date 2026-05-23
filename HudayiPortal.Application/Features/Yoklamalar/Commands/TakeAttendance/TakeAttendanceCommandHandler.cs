using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HudayiPortal.Application.Features.Yoklamalar.Commands.TakeAttendance;

public sealed class TakeAttendanceCommandHandler : IRequestHandler<TakeAttendanceCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;
	private readonly ILogger<TakeAttendanceCommandHandler> _logger;

	public TakeAttendanceCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ILogger<TakeAttendanceCommandHandler> logger)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
		_logger = logger;
	}

	public async Task<int> Handle(TakeAttendanceCommand request, CancellationToken cancellationToken)
	{
		var repo = _unitOfWork.Repository<GunlukYoklama>();

		// Mevcut kayıtları çek (aynı tarih + tür)
		var mevcutKayitlar = await repo
			.Where(y => y.Tarih == request.Tarih
						&& y.YoklamaTurId == request.YoklamaTurId
						&& y.SilindiMi != true)
			.ToDictionaryAsync(y => y.KullaniciId, cancellationToken);

		var yeniKayitlar = new List<GunlukYoklama>();

		foreach (var ogrenci in request.Ogrenciler)
		{
			if (mevcutKayitlar.TryGetValue(ogrenci.KullaniciId, out var mevcut))
			{
				// UPDATE
				mevcut.Durum = ogrenci.Durum;
				mevcut.Aciklama = ogrenci.Aciklama;
				mevcut.YoklamayiAlanPersonelId = _currentUserService.UserId;
				mevcut.GuncellenmeTarihi = DateTime.UtcNow;
				repo.Update(mevcut);
			}
			else
			{
				// INSERT
				yeniKayitlar.Add(new GunlukYoklama
				{
					KullaniciId = ogrenci.KullaniciId,
					YoklamaTurId = request.YoklamaTurId,
					Tarih = request.Tarih,
					Durum = ogrenci.Durum,
					Aciklama = ogrenci.Aciklama,
					YoklamayiAlanPersonelId = _currentUserService.UserId,
					OlusturulmaTarihi = DateTime.UtcNow,
					SilindiMi = false
				});
			}
		}

		if (yeniKayitlar.Count > 0)
			await repo.AddRangeAsync(yeniKayitlar, cancellationToken);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		_logger.LogInformation("Yoklama kaydı alındı. Tarih: {Tarih}, YoklamaTurId: {YoklamaTurId}, ToplamOgrenci: {ToplamOgrenci}", request.Tarih, request.YoklamaTurId, request.Ogrenciler.Count);

		return request.Ogrenciler.Count;
	}
}
