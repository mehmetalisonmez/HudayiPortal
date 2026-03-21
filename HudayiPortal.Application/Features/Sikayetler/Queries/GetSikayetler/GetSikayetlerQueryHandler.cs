using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetler;

public sealed class GetSikayetlerQueryHandler
	: IRequestHandler<GetSikayetlerQuery, List<SikayetDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetSikayetlerQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<SikayetDto>> Handle(GetSikayetlerQuery request, CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Repository<Sikayet>()
			.Where(s => s.SilindiMi != true)
			.Include(s => s.GonderenKullanici)
			.AsQueryable();

		if (request.GonderenKullaniciId.HasValue)
		{
			query = query.Where(s => s.GonderenKullaniciId == request.GonderenKullaniciId.Value);
		}

		if (request.Durum.HasValue)
		{
			query = query.Where(s => s.Durum == request.Durum.Value);
		}

		var result = await query
			.OrderByDescending(s => s.OlusturulmaTarihi)
			.Select(s => new SikayetDto(
				s.Id,
				$"{s.GonderenKullanici.Ad} {s.GonderenKullanici.Soyad}",
				s.Baslik,
				s.Icerik,
				s.Cevap,
				s.Durum,
				s.OlusturulmaTarihi,
				s.CevaplanmaTarihi))
			.ToListAsync(cancellationToken);

		return result;
	}
}
