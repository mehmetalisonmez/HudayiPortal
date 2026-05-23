using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.CreateBulkYemekMenu;

public sealed class CreateBulkYemekMenuCommandHandler : IRequestHandler<CreateBulkYemekMenuCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICacheService _cacheService;

	public CreateBulkYemekMenuCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
	{
		_unitOfWork = unitOfWork;
		_cacheService = cacheService;
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

		// Cache Invalidation
		var monthsToInvalidate = request.Menuler
			.Select(m => new { m.Tarih.Year, m.Tarih.Month })
			.Distinct();

		foreach (var month in monthsToInvalidate)
		{
			var cacheKey = $"yemek:menu:{month.Year}:{month.Month}";
			await _cacheService.RemoveAsync(cacheKey, cancellationToken);
		}
		await _cacheService.RemoveAsync("yemek:menu", cancellationToken);

		return entities.Count;
	}
}
