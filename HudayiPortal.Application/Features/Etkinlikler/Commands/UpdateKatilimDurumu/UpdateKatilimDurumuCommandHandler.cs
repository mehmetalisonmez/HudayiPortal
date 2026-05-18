using HudayiPortal.Application.Exceptions;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Etkinlikler.Commands.UpdateKatilimDurumu;

public sealed class UpdateKatilimDurumuCommandHandler : IRequestHandler<UpdateKatilimDurumuCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateKatilimDurumuCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdateKatilimDurumuCommand request, CancellationToken cancellationToken)
	{
		var katilim = await _unitOfWork.Repository<EtkinlikKatilimcisi>()
			.GetByIdAsync(request.KatilimciId, cancellationToken);

		if (katilim is null || katilim.SilindiMi == true)
			throw new ValidationException("Katılımcı bulunamadı.", new List<string> { $"Id={request.KatilimciId} olan katılımcı kaydı mevcut değil." });

		katilim.KatilimDurumu = request.KatilimDurumu;

		_unitOfWork.Repository<EtkinlikKatilimcisi>().Update(katilim);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
