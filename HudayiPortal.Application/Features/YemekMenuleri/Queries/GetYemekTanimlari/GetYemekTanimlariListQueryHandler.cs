using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.GetYemekTanimlari;

public sealed class GetYemekTanimlariListQueryHandler
	: IRequestHandler<GetYemekTanimlariListQuery, List<YemekTanimiListItemDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetYemekTanimlariListQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<YemekTanimiListItemDto>> Handle(
		GetYemekTanimlariListQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _unitOfWork.Repository<YemekTanimi>()
			.Where(y => y.SilindiMi != true)
			.OrderBy(y => y.KategoriId)
			.ThenBy(y => y.YemekAdi)
			.Select(y => new YemekTanimiListItemDto(y.Id, y.YemekAdi, y.KategoriId))
			.ToListAsync(cancellationToken);

		return data;
	}
}
