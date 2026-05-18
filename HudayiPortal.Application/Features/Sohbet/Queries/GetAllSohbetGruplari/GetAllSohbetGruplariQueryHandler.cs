using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Sohbet.Queries.GetAllSohbetGruplari;

public sealed class GetAllSohbetGruplariQueryHandler
	: IRequestHandler<GetAllSohbetGruplariQuery, List<SohbetGrubuDetailDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllSohbetGruplariQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<SohbetGrubuDetailDto>> Handle(
		GetAllSohbetGruplariQuery request,
		CancellationToken cancellationToken)
	{
		return await _unitOfWork.Repository<SohbetGrubu>()
			.Where(g => g.SilindiMi != true)
			.OrderBy(g => g.GrupAdi)
			.Select(g => new SohbetGrubuDetailDto(
				g.Id,
				g.GrupAdi,
				g.SorumluHocaAdi,
				g.Donem,
				g.OgrenciSohbetGruplari.Count(o => o.SilindiMi != true),
				g.Sohbetler.Count(s => s.SilindiMi != true),
				g.OlusturulmaTarihi
			))
			.ToListAsync(cancellationToken);
	}
}
