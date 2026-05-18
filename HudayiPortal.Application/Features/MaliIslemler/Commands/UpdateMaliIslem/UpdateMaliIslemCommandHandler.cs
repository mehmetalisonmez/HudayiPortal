using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.MaliIslemler.Commands.UpdateMaliIslem;

public sealed class UpdateMaliIslemCommandHandler : IRequestHandler<UpdateMaliIslemCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateMaliIslemCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(UpdateMaliIslemCommand request, CancellationToken cancellationToken)
	{
		var maliIslem = await _unitOfWork.Repository<MaliIslem>()
			.GetByIdAsync(request.Id, cancellationToken)
			?? throw new KeyNotFoundException($"Mali işlem bulunamadı. Id: {request.Id}");

		maliIslem.YonId = request.YonId;
		maliIslem.Baslik = request.Baslik;
		maliIslem.Aciklama = request.Aciklama;
		maliIslem.Tutar = request.Tutar;
		maliIslem.IslemTarihi = request.IslemTarihi;
		maliIslem.IlgiliKullaniciId = request.IlgiliKullaniciId;
		maliIslem.KategoriId = request.KategoriId;
		maliIslem.BelgeUrl = request.BelgeUrl;
		maliIslem.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<MaliIslem>().Update(maliIslem);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
	}
}
