using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Commands.CreatePersonelNobet;

public sealed class CreatePersonelNobetCommandHandler : IRequestHandler<CreatePersonelNobetCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreatePersonelNobetCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(CreatePersonelNobetCommand request, CancellationToken cancellationToken)
	{
		var nobet = new PersonelNobeti
		{
			PersonelId = request.PersonelId,
			Tarih = DateOnly.FromDateTime(request.Tarih),
			NobetTuru = request.NobetTuru,
			Aciklama = request.Aciklama,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<PersonelNobeti>().AddAsync(nobet, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return nobet.Id;
	}
}
