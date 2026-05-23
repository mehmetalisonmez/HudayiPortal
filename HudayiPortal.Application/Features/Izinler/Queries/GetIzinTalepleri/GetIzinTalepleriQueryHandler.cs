using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.Izinler.Queries.GetIzinTalepleri;

public sealed class GetIzinTalepleriQueryHandler
	: IRequestHandler<GetIzinTalepleriQuery, List<IzinDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public GetIzinTalepleriQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<List<IzinDto>> Handle(GetIzinTalepleriQuery request, CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Repository<Izin>()
			.Where(i => i.SilindiMi != true)
			.Include(i => i.Kullanici)
			.Include(i => i.IzinTuru)
			.AsQueryable();

		var currentUserId = _currentUserService.UserId;
		var roleName = _currentUserService.Role;

		if (roleName != "Admin" && roleName != "Personel")
		{
			// Öğrenciler sadece kendi izinlerini görebilir (IDOR Önlemi)
			query = query.Where(i => i.KullaniciId == currentUserId);
		}
		else if (request.KullaniciId.HasValue)
		{
			// Yönetici/Personel dilediği kullanıcının iznini filtreleyebilir
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
