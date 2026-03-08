using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.CreateYemekMenu;

public sealed class CreateYemekMenuCommandHandler : IRequestHandler<CreateYemekMenuCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateYemekMenuCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(CreateYemekMenuCommand request, CancellationToken cancellationToken)
	{
		var menu = new YemekMenusu
		{
			Tarih = request.Tarih,
			OgunTuruId = request.OgunTuruId,
			CorbaId = request.CorbaId,
			AnaYemekId = request.AnaYemekId,
			YardimciYemekId = request.YardimciYemekId,
			EkstraId = request.EkstraId,
			KahvaltiSicak1Id = request.KahvaltiSicak1Id,
			KahvaltiSicak2Id = request.KahvaltiSicak2Id,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<YemekMenusu>().AddAsync(menu, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return menu.Id;
	}
}
