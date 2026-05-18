using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.SohbetYoklamalar.Commands.TakeSohbetAttendance;

public sealed class TakeSohbetAttendanceCommandHandler : IRequestHandler<TakeSohbetAttendanceCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public TakeSohbetAttendanceCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<int> Handle(TakeSohbetAttendanceCommand request, CancellationToken cancellationToken)
	{
		var repo = _unitOfWork.Repository<SohbetYoklama>();

		// Mevcut kayıtları çek (aynı sohbet oturumu)
		var mevcutKayitlar = await repo
			.Where(sy => sy.SohbetId == request.SohbetId && sy.SilindiMi != true)
			.ToDictionaryAsync(sy => sy.KullaniciId, cancellationToken);

		var yeniKayitlar = new List<SohbetYoklama>();

		foreach (var ogrenci in request.Ogrenciler)
		{
			if (mevcutKayitlar.TryGetValue(ogrenci.KullaniciId, out var mevcut))
			{
				// UPDATE
				mevcut.KatilimDurumu = ogrenci.KatilimDurumu;
				mevcut.MazeretAciklamasi = ogrenci.MazeretAciklamasi;
				mevcut.YoklamayiAlanPersonelId = _currentUserService.UserId;
				mevcut.GuncellenmeTarihi = DateTime.UtcNow;
				repo.Update(mevcut);
			}
			else
			{
				// INSERT
				yeniKayitlar.Add(new SohbetYoklama
				{
					SohbetId = request.SohbetId,
					KullaniciId = ogrenci.KullaniciId,
					KatilimDurumu = ogrenci.KatilimDurumu,
					MazeretAciklamasi = ogrenci.MazeretAciklamasi,
					YoklamayiAlanPersonelId = _currentUserService.UserId,
					OlusturulmaTarihi = DateTime.UtcNow,
					SilindiMi = false
				});
			}
		}

		if (yeniKayitlar.Count > 0)
			await repo.AddRangeAsync(yeniKayitlar, cancellationToken);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return request.Ogrenciler.Count;
	}
}
