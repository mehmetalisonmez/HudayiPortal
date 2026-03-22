using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.CreateSohbetGrubu;

public sealed class CreateSohbetGrubuCommandHandler : IRequestHandler<CreateSohbetGrubuCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateSohbetGrubuCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(CreateSohbetGrubuCommand request, CancellationToken cancellationToken)
	{
		var grup = new SohbetGrubu
		{
			GrupAdi = request.GrupAdi,
			SorumluHocaAdi = request.SorumluHocaAdi,
			Donem = request.Donem,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<SohbetGrubu>().AddAsync(grup, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return grup.Id;
	}
}
