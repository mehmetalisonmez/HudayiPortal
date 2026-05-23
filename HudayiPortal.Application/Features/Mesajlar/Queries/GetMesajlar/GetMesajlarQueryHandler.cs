using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.Mesajlar.Queries.GetMesajlar;

public sealed class GetMesajlarQueryHandler : IRequestHandler<GetMesajlarQuery, List<MesajDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public GetMesajlarQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<List<MesajDto>> Handle(GetMesajlarQuery request, CancellationToken cancellationToken)
	{
		var currentUserId = _currentUserService.UserId;
		var roleName = _currentUserService.Role;

		var query = _unitOfWork.Repository<Mesaj>()
			.Where(m => m.SilindiMi != true)
			.Include(m => m.Gonderen)
			.AsQueryable();

		if (request.ChatGrupId.HasValue)
		{
			var groupId = request.ChatGrupId.Value;

			if (roleName != "Admin" && roleName != "Personel")
			{
				// Öğrencinin bu grubun üyesi olup olmadığını kontrol et (IDOR Önlemi)
				var isMember = await _unitOfWork.Repository<ChatGrupUyesi>()
					.AnyAsync(u => u.ChatGrupId == groupId && u.KullaniciId == currentUserId && u.SilindiMi != true, cancellationToken);

				if (!isMember)
				{
					throw new BusinessException("Bu grup sohbetine erişim yetkiniz bulunmamaktadır.");
				}
			}

			query = query.Where(m => m.ChatGrupId == groupId);
		}
		else if (request.AliciId.HasValue)
		{
			var aliciId = request.AliciId.Value;
			query = query.Where(m =>
				(m.GonderenId == currentUserId && m.AliciId == aliciId) ||
				(m.GonderenId == aliciId && m.AliciId == currentUserId));
		}
		else
		{
			query = query.Where(m => m.GonderenId == currentUserId || m.AliciId == currentUserId);
		}

		var result = await query
			.OrderBy(m => m.OlusturulmaTarihi)
			.Select(m => new MesajDto(
				m.Id,
				m.GonderenId,
				$"{m.Gonderen.Ad} {m.Gonderen.Soyad}",
				m.AliciId,
				m.ChatGrupId,
				m.MesajIcerigi,
				m.OlusturulmaTarihi))
			.ToListAsync(cancellationToken);

		return result;
	}
}
