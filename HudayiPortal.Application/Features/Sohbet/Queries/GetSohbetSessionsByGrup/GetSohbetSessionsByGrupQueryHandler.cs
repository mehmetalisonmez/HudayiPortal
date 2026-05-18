using HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetGrubuById;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SohbetEntity = HudayiPortal.Domain.Entities.Sohbet;

namespace HudayiPortal.Application.Features.Sohbet.Queries.GetSohbetSessionsByGrup;

public sealed class GetSohbetSessionsByGrupQueryHandler
	: IRequestHandler<GetSohbetSessionsByGrupQuery, List<GrupOturumDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetSohbetSessionsByGrupQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<GrupOturumDto>> Handle(
		GetSohbetSessionsByGrupQuery request,
		CancellationToken cancellationToken)
	{
		return await _unitOfWork.Repository<SohbetEntity>()
			.Where(s => s.SohbetGrupId == request.GrupId && s.SilindiMi != true)
			.OrderByDescending(s => s.Tarih)
			.Select(s => new GrupOturumDto(
				s.Id,
				s.Tarih,
				s.KonuBasligi,
				s.SohbetYoklamalari.Count(y => y.SilindiMi != true)
			))
			.ToListAsync(cancellationToken);
	}
}
