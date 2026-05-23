using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.DeleteYemekMenu;

public sealed class DeleteYemekMenuCommandHandler : IRequestHandler<DeleteYemekMenuCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICacheService _cacheService;

	public DeleteYemekMenuCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
	{
		_unitOfWork = unitOfWork;
		_cacheService = cacheService;
	}

	public async Task<Unit> Handle(DeleteYemekMenuCommand request, CancellationToken cancellationToken)
	{
		var menu = await _unitOfWork.Repository<YemekMenusu>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (menu is null)
			throw new KeyNotFoundException($"Yemek menüsü bulunamadı: {request.Id}");

		// Soft Delete — fiziksel silme yerine bayrak ile işaretleme
		menu.SilindiMi = true;
		menu.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<YemekMenusu>().Update(menu);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		// Cache Invalidation
		var cacheKey = $"yemek:menu:{menu.Tarih.Year}:{menu.Tarih.Month}";
		await _cacheService.RemoveAsync(cacheKey, cancellationToken);
		await _cacheService.RemoveAsync("yemek:menu", cancellationToken);

		return Unit.Value;
	}
}
