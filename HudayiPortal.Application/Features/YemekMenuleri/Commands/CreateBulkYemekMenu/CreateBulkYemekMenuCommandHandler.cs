using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.CreateBulkYemekMenu;

public sealed class CreateBulkYemekMenuCommandHandler : IRequestHandler<CreateBulkYemekMenuCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateBulkYemekMenuCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(CreateBulkYemekMenuCommand request, CancellationToken cancellationToken)
	{
		var entities = request.Menuler.Select(m => new YemekMenusu
		{
			Tarih = m.Tarih,
			OgunTuruId = m.OgunTuruId,
			CorbaId = m.CorbaId,
			AnaYemekId = m.AnaYemekId,
			YardimciYemekId = m.YardimciYemekId,
			EkstraId = m.EkstraId,
			KahvaltiSicak1Id = m.KahvaltiSicak1Id,
			KahvaltiSicak2Id = m.KahvaltiSicak2Id,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		}).ToList();

		await _unitOfWork.Repository<YemekMenusu>().AddRangeAsync(entities, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return entities.Count;
	}
}
