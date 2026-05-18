using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.UpdateSohbetGrubu;

public sealed class UpdateSohbetGrubuCommandHandler : IRequestHandler<UpdateSohbetGrubuCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateSohbetGrubuCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdateSohbetGrubuCommand request, CancellationToken cancellationToken)
	{
		var grup = await _unitOfWork.Repository<SohbetGrubu>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (grup is null)
			throw new KeyNotFoundException($"Sohbet grubu bulunamadı: {request.Id}");

		grup.GrupAdi = request.GrupAdi;
		grup.SorumluHocaAdi = request.SorumluHocaAdi;
		grup.Donem = request.Donem;
		grup.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<SohbetGrubu>().Update(grup);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
