using HudayiPortal.Domain.Repositories;
using MediatR;
using SohbetEntity = HudayiPortal.Domain.Entities.Sohbet;

namespace HudayiPortal.Application.Features.Sohbet.Commands.UpdateSohbetSession;

public sealed class UpdateSohbetSessionCommandHandler : IRequestHandler<UpdateSohbetSessionCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateSohbetSessionCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdateSohbetSessionCommand request, CancellationToken cancellationToken)
	{
		var sohbet = await _unitOfWork.Repository<SohbetEntity>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (sohbet is null)
			throw new KeyNotFoundException($"Sohbet oturumu bulunamadı: {request.Id}");

		sohbet.Tarih = request.Tarih;
		sohbet.KonuBasligi = request.KonuBasligi;
		sohbet.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<SohbetEntity>().Update(sohbet);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
