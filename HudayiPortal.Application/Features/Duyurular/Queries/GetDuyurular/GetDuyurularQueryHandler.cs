using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.Duyurular.Queries.GetDuyurular;

public sealed class GetDuyurularQueryHandler
	: IRequestHandler<GetDuyurularQuery, List<DuyuruListDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;
	private readonly ICacheService _cacheService;

	public GetDuyurularQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ICacheService cacheService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
		_cacheService = cacheService;
	}

	public async Task<List<DuyuruListDto>> Handle(
		GetDuyurularQuery request,
		CancellationToken cancellationToken)
	{
		const string cacheKey = "duyurular:aktif";

		var cachedResult = await _cacheService.GetAsync<List<DuyuruListDto>>(cacheKey, cancellationToken);
		if (cachedResult != null)
		{
			return cachedResult;
		}

		var role   = _currentUserService.Role;
		var roleId = _currentUserService.RoleId;

		var query = _unitOfWork.Repository<Duyuru>()
			.Where(d => d.SilindiMi == false);

		// Öğrenci yalnızca Genel (null) veya kendi rolüne ait duyuruları görür
		if (role == "Öğrenci")
		{
			query = query.Where(d => d.HedefRolId == null || d.HedefRolId == roleId);
		}
		// Admin ve Personel tüm duyuruları görür — ek filtre yok

		var result = await query
			.OrderByDescending(d => d.OlusturulmaTarihi)
			.Select(d => new DuyuruListDto(
				d.Id,
				d.Baslik,
				d.Icerik,
				d.YayinTarihi,
				d.OlusturulmaTarihi,
				d.HedefRolId,
				d.HedefRol != null ? d.HedefRol.RolAdi : null))
			.ToListAsync(cancellationToken);

		await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromHours(1), cancellationToken);

		return result;
	}
}
