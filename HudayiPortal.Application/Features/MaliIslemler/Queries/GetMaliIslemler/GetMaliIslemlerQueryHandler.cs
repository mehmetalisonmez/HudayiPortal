using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.MaliIslemler.Queries.GetMaliIslemler;

public sealed class GetMaliIslemlerQueryHandler
	: IRequestHandler<GetMaliIslemlerQuery, List<MaliIslemDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetMaliIslemlerQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<MaliIslemDto>> Handle(GetMaliIslemlerQuery request, CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Repository<MaliIslem>()
			.Where(m => m.SilindiMi == false)
			.Include(m => m.Yon)
			.Include(m => m.IlgiliKullanici)
			.AsQueryable();

		if (request.YonId.HasValue)
		{
			query = query.Where(m => m.YonId == request.YonId.Value);
		}

		if (request.BaslangicTarihi.HasValue)
		{
			query = query.Where(m => m.IslemTarihi >= request.BaslangicTarihi.Value);
		}

		if (request.BitisTarihi.HasValue)
		{
			query = query.Where(m => m.IslemTarihi <= request.BitisTarihi.Value);
		}

		var result = await query
			.OrderByDescending(m => m.IslemTarihi)
			.Select(m => new MaliIslemDto(
				m.Id,
				m.Yon.YonAdi,
				m.Baslik,
				m.Aciklama,
				m.Tutar,
				m.IslemTarihi,
				m.IlgiliKullanici != null ? $"{m.IlgiliKullanici.Ad} {m.IlgiliKullanici.Soyad}" : null))
			.ToListAsync(cancellationToken);

		return result;
	}
}
