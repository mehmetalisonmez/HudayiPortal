using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikKatilimcilari;

public sealed class GetEtkinlikKatilimcilariQueryHandler
	: IRequestHandler<GetEtkinlikKatilimcilariQuery, List<KatilimciDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetEtkinlikKatilimcilariQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<KatilimciDto>> Handle(GetEtkinlikKatilimcilariQuery request, CancellationToken cancellationToken)
	{
		var result = await _unitOfWork.Repository<EtkinlikKatilimcisi>()
			.Where(k => k.EtkinlikId == request.EtkinlikId && k.SilindiMi != true)
			.OrderBy(k => k.Kullanici.Ad)
			.Select(k => new KatilimciDto(
				k.Id,
				k.KullaniciId,
				k.Kullanici.Ad,
				k.Kullanici.Soyad,
				k.KatilimDurumu,
				k.OlusturulmaTarihi
			))
			.ToListAsync(cancellationToken);

		return result;
	}
}
