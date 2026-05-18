using HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetPersonelNobetleri;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetNobetler;

public sealed class GetNobetlerQueryHandler : IRequestHandler<GetNobetlerQuery, List<PersonelNobetDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetNobetlerQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<PersonelNobetDto>> Handle(GetNobetlerQuery request, CancellationToken cancellationToken)
	{
		return await _unitOfWork.Repository<PersonelNobeti>()
			.Where(n => n.SilindiMi != true
					 && n.Tarih >= request.StartDate
					 && n.Tarih <= request.EndDate)
			.Include(n => n.Personel)
			.OrderBy(n => n.Tarih)
			.ThenBy(n => n.Personel.Ad)
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
