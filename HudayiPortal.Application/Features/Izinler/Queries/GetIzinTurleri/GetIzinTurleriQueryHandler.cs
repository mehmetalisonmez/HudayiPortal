using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Izinler.Queries.GetIzinTurleri;

public sealed class GetIzinTurleriQueryHandler
	: IRequestHandler<GetIzinTurleriQuery, List<IzinTuruDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetIzinTurleriQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<IzinTuruDto>> Handle(GetIzinTurleriQuery request, CancellationToken cancellationToken)
	{
		var result = await _unitOfWork.Repository<IzinTuru>()
			.Where(t => t.SilindiMi != true)
			.OrderBy(t => t.TurAdi)
			.Select(t => new IzinTuruDto(t.Id, t.TurAdi))
			.ToListAsync(cancellationToken);

		return result;
	}
}
