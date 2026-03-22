using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

		var query = _unitOfWork.Repository<Mesaj>()
			.Where(m => m.SilindiMi != true)
			.Include(m => m.Gonderen)
			.AsQueryable();

		if (request.ChatGrupId.HasValue)
		{
			query = query.Where(m => m.ChatGrupId == request.ChatGrupId.Value);
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
