using HudayiPortal.Application.Features.Duyurular.Queries.GetAktifDuyurular;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Duyurular.Queries.GetAllDuyurular;

public sealed class GetAllDuyurularQueryHandler
	: IRequestHandler<GetAllDuyurularQuery, List<DuyuruDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllDuyurularQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<DuyuruDto>> Handle(GetAllDuyurularQuery request, CancellationToken cancellationToken)
	{
		// Soft-delete edilmemiş TÜM duyuruları getir (süresi dolmuş dahil)
		var duyurular = await _unitOfWork.Repository<Duyuru>()
			.Where(d => d.SilindiMi == false)
			.OrderByDescending(d => d.OlusturulmaTarihi)
			.Select(d => new DuyuruDto(
				d.Id,
				d.Baslik,
				d.Icerik,
				d.GecerlilikTarihi,
				d.OlusturulmaTarihi))
			.ToListAsync(cancellationToken);

		return duyurular;
	}
}
