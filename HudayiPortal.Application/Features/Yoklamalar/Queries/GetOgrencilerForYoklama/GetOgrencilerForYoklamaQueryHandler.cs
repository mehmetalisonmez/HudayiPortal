using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetOgrencilerForYoklama;

public sealed class GetOgrencilerForYoklamaQueryHandler
	: IRequestHandler<GetOgrencilerForYoklamaQuery, List<OgrenciYoklamaDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetOgrencilerForYoklamaQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<OgrenciYoklamaDto>> Handle(
		GetOgrencilerForYoklamaQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.RolId == 1
						&& k.AktifMi == true
						&& k.SilindiMi != true)
			.Include(k => k.Oda)
			.OrderBy(k => k.Oda != null ? k.Oda.OdaNo : "")
			.ThenBy(k => k.Soyad)
			.ThenBy(k => k.Ad)
			.Select(k => new OgrenciYoklamaDto(
				k.Id,
				k.Ad,
				k.Soyad,
				k.OdaId,
				k.Oda != null ? k.Oda.OdaNo : null))
			.ToListAsync(cancellationToken);

		return data;
	}
}
