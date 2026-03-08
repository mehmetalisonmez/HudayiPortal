using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.YemekMenuleri.Queries.GetAylikYemekMenu;

public sealed class GetAylikYemekMenuQueryHandler
	: IRequestHandler<GetAylikYemekMenuQuery, List<YemekMenuDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAylikYemekMenuQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<YemekMenuDto>> Handle(
		GetAylikYemekMenuQuery request,
		CancellationToken cancellationToken)
	{
		var baslangic = new DateOnly(request.Yil, request.Ay, 1);
		var bitis = baslangic.AddMonths(1).AddDays(-1);

		var data = await _unitOfWork.Repository<YemekMenusu>()
			.Where(m => m.SilindiMi != true
						&& m.Tarih >= baslangic
						&& m.Tarih <= bitis)
			.Include(m => m.Corba)
			.Include(m => m.AnaYemek)
			.Include(m => m.YardimciYemek)
			.Include(m => m.Ekstra)
			.Include(m => m.KahvaltiSicak1)
			.Include(m => m.KahvaltiSicak2)
			.OrderBy(m => m.Tarih)
			.ThenBy(m => m.OgunTuruId)
			.Select(m => new YemekMenuDto(
				m.Id,
				m.Tarih,
				m.OgunTuruId,
				m.Corba != null ? m.Corba.YemekAdi : null,
				m.AnaYemek != null ? m.AnaYemek.YemekAdi : null,
				m.YardimciYemek != null ? m.YardimciYemek.YemekAdi : null,
				m.Ekstra != null ? m.Ekstra.YemekAdi : null,
				m.KahvaltiSicak1 != null ? m.KahvaltiSicak1.YemekAdi : null,
				m.KahvaltiSicak2 != null ? m.KahvaltiSicak2.YemekAdi : null))
			.ToListAsync(cancellationToken);

		return data;
	}
}
