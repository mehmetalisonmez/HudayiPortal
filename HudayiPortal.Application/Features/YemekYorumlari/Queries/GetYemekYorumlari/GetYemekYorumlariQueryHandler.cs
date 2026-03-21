using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.YemekYorumlari.Queries.GetYemekYorumlari;

public sealed class GetYemekYorumlariQueryHandler
	: IRequestHandler<GetYemekYorumlariQuery, List<YemekYorumDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetYemekYorumlariQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<YemekYorumDto>> Handle(
		GetYemekYorumlariQuery request,
		CancellationToken cancellationToken)
	{
		var yorumlar = await _unitOfWork.Repository<YemekYorumu>()
			.Where(y => y.YemekMenuId == request.YemekMenuId && y.SilindiMi == false)
			.Include(y => y.Kullanici)
			.OrderByDescending(y => y.OlusturulmaTarihi)
			.Select(y => new YemekYorumDto(
				y.Id,
				y.YemekMenuId,
				y.KullaniciId,
				$"{y.Kullanici.Ad} {y.Kullanici.Soyad}",
				y.YorumMetni,
				y.Puan,
				y.OlusturulmaTarihi))
			.ToListAsync(cancellationToken);

		return yorumlar;
	}
}
