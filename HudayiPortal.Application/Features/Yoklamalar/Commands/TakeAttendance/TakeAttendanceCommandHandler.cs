using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Yoklamalar.Commands.TakeAttendance;

public sealed class TakeAttendanceCommandHandler : IRequestHandler<TakeAttendanceCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public TakeAttendanceCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(TakeAttendanceCommand request, CancellationToken cancellationToken)
	{
		var entities = request.Ogrenciler.Select(o => new GunlukYoklama
		{
			KullaniciId = o.KullaniciId,
			YoklamaTurId = request.YoklamaTurId,
			Tarih = request.Tarih,
			Durum = o.Durum,
			Aciklama = o.Aciklama,
			YoklamayiAlanPersonelId = 1,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		}).ToList();

		await _unitOfWork.Repository<GunlukYoklama>().AddRangeAsync(entities, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return entities.Count;
	}
}
