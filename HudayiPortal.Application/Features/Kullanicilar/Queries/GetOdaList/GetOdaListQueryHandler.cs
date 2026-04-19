using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Kullanicilar.Queries.GetOdaList;

public sealed class GetOdaListQueryHandler : IRequestHandler<GetOdaListQuery, List<OdaListDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetOdaListQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<OdaListDto>> Handle(GetOdaListQuery request, CancellationToken cancellationToken)
	{
		var odalar = await _unitOfWork.Repository<Oda>()
			.Where(o => o.SilindiMi != true)
			.OrderBy(o => o.Kat)
			.ThenBy(o => o.OdaNo)
			.Select(o => new OdaListDto(
				o.Id,
				o.OdaNo,
				o.Kat,
				o.Kapasite))
			.ToListAsync(cancellationToken);

		return odalar;
	}
}
