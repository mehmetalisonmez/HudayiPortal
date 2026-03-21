using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Izinler.Queries.GetIzinTalepleri;

public sealed class GetIzinTalepleriQueryHandler
	: IRequestHandler<GetIzinTalepleriQuery, List<IzinDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetIzinTalepleriQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<IzinDto>> Handle(GetIzinTalepleriQuery request, CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Repository<Izin>()
			.Where(i => i.SilindiMi != true)
			.Include(i => i.Kullanici)
			.Include(i => i.IzinTuru)
			.AsQueryable();

		if (request.KullaniciId.HasValue)
		{
			query = query.Where(i => i.KullaniciId == request.KullaniciId.Value);
		}

		if (request.OnayDurumu.HasValue)
		{
			query = query.Where(i => i.OnayDurumu == request.OnayDurumu.Value);
		}

		var result = await query
			.OrderByDescending(i => i.OlusturulmaTarihi)
			.Select(i => new IzinDto(
				i.Id,
				$"{i.Kullanici.Ad} {i.Kullanici.Soyad}",
				i.IzinTuru.TurAdi,
				i.BaslangicTarihi,
				i.BitisTarihi,
				i.GidecegiAdres,
				i.OnayDurumu))
			.ToListAsync(cancellationToken);

		return result;
	}
}
