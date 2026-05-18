using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.YemekTanimlari.Queries.GetAllYemekTanimlari;

public sealed class GetAllYemekTanimlariQueryHandler
	: IRequestHandler<GetAllYemekTanimlariQuery, List<YemekTanimiDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllYemekTanimlariQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<YemekTanimiDto>> Handle(
		GetAllYemekTanimlariQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _unitOfWork.Repository<YemekTanimi>()
			.Where(y => y.SilindiMi != true)
			.Include(y => y.Kategori)
			.OrderBy(y => y.KategoriId)
			.ThenBy(y => y.YemekAdi)
			.Select(y => new YemekTanimiDto(
				y.Id,
				y.YemekAdi,
				y.KategoriId,
				y.Kategori.KategoriAdi,
				y.Kalori,
				y.ResimUrl))
			.ToListAsync(cancellationToken);

		return data;
	}
}
