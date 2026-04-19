using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Commands.UpdateDuyuru;

public sealed class UpdateDuyuruCommandHandler : IRequestHandler<UpdateDuyuruCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateDuyuruCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdateDuyuruCommand request, CancellationToken cancellationToken)
	{
		var duyuru = await _unitOfWork.Repository<Duyuru>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (duyuru is null)
			throw new KeyNotFoundException($"Duyuru bulunamadı: {request.Id}");

		duyuru.Baslik = request.Baslik;
		duyuru.Icerik = request.Icerik;
		duyuru.GecerlilikTarihi = request.GecerlilikTarihi;
		duyuru.HedefRolId = request.HedefRolId;
		duyuru.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Duyuru>().Update(duyuru);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
