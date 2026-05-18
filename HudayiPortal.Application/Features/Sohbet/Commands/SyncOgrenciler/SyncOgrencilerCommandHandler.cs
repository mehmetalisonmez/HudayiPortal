using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Sohbet.Commands.SyncOgrenciler;

public sealed class SyncOgrencilerCommandHandler : IRequestHandler<SyncOgrencilerCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public SyncOgrencilerCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(SyncOgrencilerCommand request, CancellationToken cancellationToken)
	{
		var repo = _unitOfWork.Repository<OgrenciSohbetGrubu>();

		// Tüm kayıtları çek (soft-delete dahil — geri açma senaryosu için)
		var mevcutKayitlar = await repo
			.Where(o => o.SohbetGrupId == request.SohbetGrupId)
			.ToListAsync(cancellationToken);

		var istenenSet = new HashSet<int>(request.KullaniciIds);
		var now = DateTime.UtcNow;

		foreach (var kayit in mevcutKayitlar)
		{
			if (istenenSet.Contains(kayit.KullaniciId))
			{
				// Daha önce soft-delete edilmiş ama tekrar ekleniyor
				if (kayit.SilindiMi == true)
				{
					kayit.SilindiMi = false;
					kayit.AtanmaTarihi = now;
					repo.Update(kayit);
				}
				// Zaten aktif — bir şey yapma
				istenenSet.Remove(kayit.KullaniciId);
			}
			else
			{
				// Listeden çıkarılmış — soft delete
				if (kayit.SilindiMi != true)
				{
					kayit.SilindiMi = true;
					repo.Update(kayit);
				}
			}
		}

		// Kalan ID'ler yeni atamalar
		if (istenenSet.Count > 0)
		{
			var yeniKayitlar = istenenSet.Select(kullaniciId => new OgrenciSohbetGrubu
			{
				KullaniciId = kullaniciId,
				SohbetGrupId = request.SohbetGrupId,
				AtanmaTarihi = now,
				SilindiMi = false
			});

			await repo.AddRangeAsync(yeniKayitlar, cancellationToken);
		}

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
