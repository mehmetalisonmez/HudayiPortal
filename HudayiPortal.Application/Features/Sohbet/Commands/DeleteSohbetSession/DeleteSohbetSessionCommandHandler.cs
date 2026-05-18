using HudayiPortal.Domain.Repositories;
using MediatR;
using SohbetEntity = HudayiPortal.Domain.Entities.Sohbet;

namespace HudayiPortal.Application.Features.Sohbet.Commands.DeleteSohbetSession;

public sealed class DeleteSohbetSessionCommandHandler : IRequestHandler<DeleteSohbetSessionCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteSohbetSessionCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteSohbetSessionCommand request, CancellationToken cancellationToken)
	{
		var sohbet = await _unitOfWork.Repository<SohbetEntity>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (sohbet is null)
			throw new KeyNotFoundException($"Sohbet oturumu bulunamadı: {request.Id}");

		sohbet.SilindiMi = true;
		sohbet.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<SohbetEntity>().Update(sohbet);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
