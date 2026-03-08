using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Mesajlar.Queries.GetMesajGecmisi;

public sealed class GetMesajGecmisiQueryHandler
	: IRequestHandler<GetMesajGecmisiQuery, List<MesajDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetMesajGecmisiQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<MesajDto>> Handle(
		GetMesajGecmisiQuery request,
		CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Repository<Mesaj>()
			.Where(m => m.SilindiMi != true);

		if (request.ChatGrupId.HasValue)
		{
			query = query.Where(m => m.ChatGrupId == request.ChatGrupId.Value);
		}
		else if (request.AliciId.HasValue)
		{
			var aliciId = request.AliciId.Value;
			query = query.Where(m =>
				(m.GonderenId == 1 && m.AliciId == aliciId) ||
				(m.GonderenId == aliciId && m.AliciId == 1));
		}

		var mesajlar = await query
			.OrderBy(m => m.OlusturulmaTarihi)
			.Select(m => new MesajDto(
				m.Id,
				m.GonderenId,
				m.AliciId,
				m.ChatGrupId,
				m.MesajIcerigi,
				m.OkunduMu,
				m.OlusturulmaTarihi))
			.ToListAsync(cancellationToken);

		return mesajlar;
	}
}
