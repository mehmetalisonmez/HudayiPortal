using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetPersonelNobetleri;

public sealed class GetPersonelNobetleriQueryHandler
	: IRequestHandler<GetPersonelNobetleriQuery, List<PersonelNobetDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetPersonelNobetleriQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<PersonelNobetDto>> Handle(GetPersonelNobetleriQuery request, CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Repository<PersonelNobeti>()
			.Where(p => p.SilindiMi == false)
			.Include(p => p.Personel)
			.AsQueryable();

		if (request.PersonelId.HasValue)
		{
			query = query.Where(p => p.PersonelId == request.PersonelId.Value);
		}

		if (request.BaslangicTarihi.HasValue)
		{
			var baslangic = DateOnly.FromDateTime(request.BaslangicTarihi.Value);
			query = query.Where(p => p.Tarih >= baslangic);
		}

		if (request.BitisTarihi.HasValue)
		{
			var bitis = DateOnly.FromDateTime(request.BitisTarihi.Value);
			query = query.Where(p => p.Tarih <= bitis);
		}

		var result = await query
			.OrderByDescending(p => p.Tarih)
			.Select(p => new PersonelNobetDto(
				p.Id,
				p.PersonelId,
				$"{p.Personel.Ad} {p.Personel.Soyad}",
				p.Tarih.ToDateTime(TimeOnly.MinValue),
				p.NobetTuru,
				p.Aciklama))
			.ToListAsync(cancellationToken);

		return result;
	}
}
