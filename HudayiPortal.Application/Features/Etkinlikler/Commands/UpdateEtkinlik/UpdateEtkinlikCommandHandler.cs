using HudayiPortal.Application.Exceptions;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.UpdateEtkinlik;

public sealed class UpdateEtkinlikCommandHandler : IRequestHandler<UpdateEtkinlikCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateEtkinlikCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdateEtkinlikCommand request, CancellationToken cancellationToken)
	{
		var etkinlik = await _unitOfWork.Repository<Etkinlik>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (etkinlik is null || etkinlik.SilindiMi == true)
			throw new ValidationException("Etkinlik bulunamadı.", new List<string> { $"Id={request.Id} olan etkinlik mevcut değil." });

		etkinlik.Baslik = request.Baslik;
		etkinlik.Aciklama = request.Aciklama;
		etkinlik.BaslangicTarihi = request.BaslangicTarihi;
		etkinlik.BitisTarihi = request.BitisTarihi;
		etkinlik.SonKayitTarihi = request.SonKayitTarihi;
		etkinlik.Ucret = request.Ucret;
		etkinlik.ZorunluMu = request.ZorunluMu;
		etkinlik.ResimUrl = request.ResimUrl;
		etkinlik.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Etkinlik>().Update(etkinlik);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
