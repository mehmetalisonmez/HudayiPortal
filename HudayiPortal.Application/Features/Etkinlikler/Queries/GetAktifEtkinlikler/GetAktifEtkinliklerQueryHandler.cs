using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetAktifEtkinlikler;

public sealed class GetAktifEtkinliklerQueryHandler
	: IRequestHandler<GetAktifEtkinliklerQuery, List<EtkinlikDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAktifEtkinliklerQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<EtkinlikDto>> Handle(GetAktifEtkinliklerQuery request, CancellationToken cancellationToken)
	{
		var now = DateTime.UtcNow;

		var result = await _unitOfWork.Repository<Etkinlik>()
			.Where(e => e.SilindiMi == false && e.BitisTarihi >= now)
			.OrderBy(e => e.BaslangicTarihi)
			.Select(e => new EtkinlikDto(
				e.Id,
				e.Baslik,
				e.Aciklama,
				e.BaslangicTarihi,
				e.BitisTarihi,
				e.SonKayitTarihi,
				e.Ucret,
				e.ZorunluMu,
				e.ResimUrl))
			.ToListAsync(cancellationToken);

		return result;
	}
}
