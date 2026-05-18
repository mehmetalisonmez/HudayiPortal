using HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetler;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetById;

public sealed class GetSikayetByIdQueryHandler
	: IRequestHandler<GetSikayetByIdQuery, SikayetDto?>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetSikayetByIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<SikayetDto?> Handle(GetSikayetByIdQuery request, CancellationToken cancellationToken)
	{
		var result = await _unitOfWork.Repository<Sikayet>()
			.Where(s => s.Id == request.Id && s.SilindiMi != true)
			.Include(s => s.GonderenKullanici)
			.Select(s => new SikayetDto(
				s.Id,
				$"{s.GonderenKullanici.Ad} {s.GonderenKullanici.Soyad}",
				s.Baslik,
				s.Icerik,
				s.Cevap,
				s.Durum,
				s.OlusturulmaTarihi,
				s.CevaplanmaTarihi))
			.FirstOrDefaultAsync(cancellationToken);

		return result;
	}
}
