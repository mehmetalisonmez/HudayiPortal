using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Commands.DeletePersonelNobet;

public sealed class DeletePersonelNobetCommandHandler : IRequestHandler<DeletePersonelNobetCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeletePersonelNobetCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeletePersonelNobetCommand request, CancellationToken cancellationToken)
	{
		var nobet = await _unitOfWork.Repository<PersonelNobeti>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (nobet is null)
			throw new KeyNotFoundException($"Nöbet kaydı bulunamadı: {request.Id}");

		nobet.SilindiMi = true;
		nobet.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<PersonelNobeti>().Update(nobet);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
