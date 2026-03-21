using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Duyurular.Queries.GetAktifDuyurular;

public sealed class GetAktifDuyurularQueryHandler
	: IRequestHandler<GetAktifDuyurularQuery, List<DuyuruDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAktifDuyurularQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<DuyuruDto>> Handle(GetAktifDuyurularQuery request, CancellationToken cancellationToken)
	{
		var now = DateTime.UtcNow;

		var duyurular = await _unitOfWork.Repository<Duyuru>()
			.Where(d => d.SilindiMi == false && (!d.GecerlilikTarihi.HasValue || d.GecerlilikTarihi >= now))
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
