using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using SohbetEntity = HudayiPortal.Domain.Entities.Sohbet;

namespace HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetSession;

public sealed class CreateSohbetSessionCommandHandler : IRequestHandler<CreateSohbetSessionCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateSohbetSessionCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(CreateSohbetSessionCommand request, CancellationToken cancellationToken)
	{
		var sohbet = new SohbetEntity
		{
			SohbetGrupId = request.SohbetGrupId,
			Tarih = request.Tarih,
			KonuBasligi = request.KonuBasligi,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<SohbetEntity>().AddAsync(sohbet, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return sohbet.Id;
	}
}
