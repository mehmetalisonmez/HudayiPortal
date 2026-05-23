using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.Duyurular.Commands.UpdateDuyuru;

public sealed class UpdateDuyuruCommandHandler : IRequestHandler<UpdateDuyuruCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICacheService _cacheService;

	public UpdateDuyuruCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
	{
		_unitOfWork = unitOfWork;
		_cacheService = cacheService;
	}

	public async Task<Unit> Handle(UpdateDuyuruCommand request, CancellationToken cancellationToken)
	{
		var duyuru = await _unitOfWork.Repository<Duyuru>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (duyuru is null)
			throw new KeyNotFoundException($"Duyuru bulunamadı: {request.Id}");

		duyuru.Baslik = request.Baslik;
		duyuru.Icerik = request.Icerik;
		duyuru.YayinTarihi = request.YayinTarihi;
		duyuru.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Duyuru>().Update(duyuru);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		// Cache Invalidation
		await _cacheService.RemoveAsync("duyurular:aktif", cancellationToken);

		return Unit.Value;
	}
}
