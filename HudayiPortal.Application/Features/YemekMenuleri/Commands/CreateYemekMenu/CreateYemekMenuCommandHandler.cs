using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.CreateYemekMenu;

public sealed class CreateYemekMenuCommandHandler : IRequestHandler<CreateYemekMenuCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICacheService _cacheService;

	public CreateYemekMenuCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
	{
		_unitOfWork = unitOfWork;
		_cacheService = cacheService;
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

		// Cache Invalidation
		var cacheKey = $"yemek:menu:{request.Tarih.Year}:{request.Tarih.Month}";
		await _cacheService.RemoveAsync(cacheKey, cancellationToken);
		await _cacheService.RemoveAsync("yemek:menu", cancellationToken);

		return menu.Id;
	}
}
