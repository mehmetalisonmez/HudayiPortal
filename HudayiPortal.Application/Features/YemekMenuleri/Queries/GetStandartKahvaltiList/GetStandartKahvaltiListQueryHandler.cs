using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.GetStandartKahvaltiList;

public sealed class GetStandartKahvaltiListQueryHandler
	: IRequestHandler<GetStandartKahvaltiListQuery, List<StandartKahvaltiDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetStandartKahvaltiListQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<StandartKahvaltiDto>> Handle(
		GetStandartKahvaltiListQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _unitOfWork.Repository<StandartKahvaltiUrunu>()
			.Where(s => s.SilindiMi != true && s.AktifMi == true)
			.Include(s => s.YemekTanim)
			.OrderBy(s => s.YemekTanim.YemekAdi)
			.Select(s => new StandartKahvaltiDto(s.Id, s.YemekTanim.YemekAdi))
			.ToListAsync(cancellationToken);

		return data;
	}
}
