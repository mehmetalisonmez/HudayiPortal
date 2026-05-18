using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetYoklamaTurleri;

public sealed class GetYoklamaTurleriQueryHandler
	: IRequestHandler<GetYoklamaTurleriQuery, List<YoklamaTuruDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetYoklamaTurleriQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<YoklamaTuruDto>> Handle(
		GetYoklamaTurleriQuery request,
		CancellationToken cancellationToken)
	{
		return await _unitOfWork.Repository<YoklamaTuru>()
			.Where(t => t.SilindiMi != true)
			.OrderBy(t => t.Id)
			.Select(t => new YoklamaTuruDto(t.Id, t.TurAdi))
			.ToListAsync(cancellationToken);
	}
}
