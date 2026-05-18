using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.UpdateYemekMenu;

public sealed class UpdateYemekMenuCommandHandler : IRequestHandler<UpdateYemekMenuCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateYemekMenuCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdateYemekMenuCommand request, CancellationToken cancellationToken)
	{
		var menu = await _unitOfWork.Repository<YemekMenusu>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (menu is null)
			throw new KeyNotFoundException($"Yemek menüsü bulunamadı: {request.Id}");

		menu.Tarih = request.Tarih;
		menu.OgunTuruId = request.OgunTuruId;
		menu.CorbaId = request.CorbaId;
		menu.AnaYemekId = request.AnaYemekId;
		menu.YardimciYemekId = request.YardimciYemekId;
		menu.EkstraId = request.EkstraId;
		menu.KahvaltiSicak1Id = request.KahvaltiSicak1Id;
		menu.KahvaltiSicak2Id = request.KahvaltiSicak2Id;
		menu.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<YemekMenusu>().Update(menu);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
