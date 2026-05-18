using HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetler;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Sikayetler.Queries.GetMySikayetler;

public sealed class GetMySikayetlerQueryHandler
	: IRequestHandler<GetMySikayetlerQuery, List<SikayetDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public GetMySikayetlerQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<List<SikayetDto>> Handle(GetMySikayetlerQuery request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId;

		var result = await _unitOfWork.Repository<Sikayet>()
			.Where(s => s.GonderenKullaniciId == userId && s.SilindiMi != true)
			.Include(s => s.GonderenKullanici)
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
