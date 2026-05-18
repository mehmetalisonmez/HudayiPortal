using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetSohbetGruplari;

public sealed class GetSohbetGruplariQueryHandler
	: IRequestHandler<GetSohbetGruplariQuery, List<SohbetGrubuDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetSohbetGruplariQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<SohbetGrubuDto>> Handle(
		GetSohbetGruplariQuery request,
		CancellationToken cancellationToken)
	{
		return await _unitOfWork.Repository<SohbetGrubu>()
			.Where(g => g.SilindiMi != true)
			.OrderBy(g => g.GrupAdi)
			.Select(g => new SohbetGrubuDto(g.Id, g.GrupAdi))
			.ToListAsync(cancellationToken);
	}
}
