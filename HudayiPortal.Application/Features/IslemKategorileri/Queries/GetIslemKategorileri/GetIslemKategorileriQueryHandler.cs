using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IslemKategorileriEntity = HudayiPortal.Domain.Entities.IslemKategorileri;

namespace HudayiPortal.Application.Features.IslemKategorileri.Queries.GetIslemKategorileri;

public sealed class GetIslemKategorileriQueryHandler
	: IRequestHandler<GetIslemKategorileriQuery, List<IslemKategorisiDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetIslemKategorileriQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<IslemKategorisiDto>> Handle(
		GetIslemKategorileriQuery request,
		CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Repository<IslemKategorileriEntity>()
			.Where(k => k.SilindiMi != true)
			.Include(k => k.Yon)
			.AsQueryable();

		if (request.YonId.HasValue)
		{
			query = query.Where(k => k.YonId == request.YonId.Value);
		}

		var result = await query
			.OrderBy(k => k.YonId)
			.ThenBy(k => k.KategoriAdi)
			.Select(k => new IslemKategorisiDto(
				k.Id,
				k.KategoriAdi,
				k.YonId,
				k.Yon.YonAdi))
			.ToListAsync(cancellationToken);

		return result;
	}
}
