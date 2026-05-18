using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.DeleteSohbetGrubu;

public sealed class DeleteSohbetGrubuCommandHandler : IRequestHandler<DeleteSohbetGrubuCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteSohbetGrubuCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteSohbetGrubuCommand request, CancellationToken cancellationToken)
	{
		var grup = await _unitOfWork.Repository<SohbetGrubu>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (grup is null)
			throw new KeyNotFoundException($"Sohbet grubu bulunamadı: {request.Id}");

		grup.SilindiMi = true;
		grup.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<SohbetGrubu>().Update(grup);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
