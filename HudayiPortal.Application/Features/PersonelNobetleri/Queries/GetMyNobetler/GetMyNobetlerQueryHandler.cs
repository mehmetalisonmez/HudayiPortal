using HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetPersonelNobetleri;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetMyNobetler;

public sealed class GetMyNobetlerQueryHandler : IRequestHandler<GetMyNobetlerQuery, List<PersonelNobetDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public GetMyNobetlerQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<List<PersonelNobetDto>> Handle(GetMyNobetlerQuery request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId;

		var query = _unitOfWork.Repository<PersonelNobeti>()
			.Where(n => n.PersonelId == userId && n.SilindiMi != true)
			.Include(n => n.Personel)
			.AsQueryable();

		if (request.StartDate.HasValue)
			query = query.Where(n => n.Tarih >= request.StartDate.Value);

		if (request.EndDate.HasValue)
			query = query.Where(n => n.Tarih <= request.EndDate.Value);

		return await query
			.OrderBy(n => n.Tarih)
			.Select(n => new PersonelNobetDto(
				n.Id,
				n.PersonelId,
				$"{n.Personel.Ad} {n.Personel.Soyad}",
				n.Tarih.ToDateTime(TimeOnly.MinValue),
				n.NobetTuru,
				n.Aciklama))
			.ToListAsync(cancellationToken);
	}
}
