using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Commands.UpdatePersonelNobet;

public sealed class UpdatePersonelNobetCommandHandler : IRequestHandler<UpdatePersonelNobetCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdatePersonelNobetCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdatePersonelNobetCommand request, CancellationToken cancellationToken)
	{
		var nobet = await _unitOfWork.Repository<PersonelNobeti>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (nobet is null)
			throw new KeyNotFoundException($"Nöbet kaydı bulunamadı: {request.Id}");

		nobet.PersonelId = request.PersonelId;
		nobet.Tarih = request.Tarih;
		nobet.NobetTuru = request.NobetTuru;
		nobet.Aciklama = request.Aciklama;
		nobet.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<PersonelNobeti>().Update(nobet);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
