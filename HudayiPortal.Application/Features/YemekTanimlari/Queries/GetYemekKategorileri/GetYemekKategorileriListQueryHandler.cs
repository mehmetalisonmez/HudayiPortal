using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.YemekTanimlari.Queries.GetYemekKategorileri;

public sealed class GetYemekKategorileriListQueryHandler
	: IRequestHandler<GetYemekKategorileriListQuery, List<YemekKategorisiDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetYemekKategorileriListQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<YemekKategorisiDto>> Handle(
		GetYemekKategorileriListQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _unitOfWork.Repository<YemekKategorisi>()
			.Where(k => k.SilindiMi != true)
			.OrderBy(k => k.Id)
			.Select(k => new YemekKategorisiDto(k.Id, k.KategoriAdi))
			.ToListAsync(cancellationToken);

		return data;
	}
}
