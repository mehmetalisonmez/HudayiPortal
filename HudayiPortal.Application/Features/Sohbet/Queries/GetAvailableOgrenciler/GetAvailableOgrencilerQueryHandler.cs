using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Sohbet.Queries.GetAvailableOgrenciler;

public sealed class GetAvailableOgrencilerQueryHandler
	: IRequestHandler<GetAvailableOgrencilerQuery, List<AvailableOgrenciDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAvailableOgrencilerQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<AvailableOgrenciDto>> Handle(
		GetAvailableOgrencilerQuery request,
		CancellationToken cancellationToken)
	{
		// Bu gruba atanmış aktif öğrenci ID'leri
		var assignedIds = await _unitOfWork.Repository<OgrenciSohbetGrubu>()
			.Where(o => o.SohbetGrupId == request.SohbetGrupId && o.SilindiMi != true)
			.Select(o => o.KullaniciId)
			.ToListAsync(cancellationToken);

		var assignedSet = new HashSet<int>(assignedIds);

		// Tüm aktif öğrenciler (RolId=1)
		var ogrenciler = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.RolId == 1
				&& k.AktifMi == true
				&& k.SilindiMi != true)
			.OrderBy(k => k.Soyad)
			.ThenBy(k => k.Ad)
			.Select(k => new
			{
				k.Id,
				k.Ad,
				k.Soyad,
				OdaNo = k.Oda != null ? k.Oda.OdaNo : null
			})
			.ToListAsync(cancellationToken);

		return ogrenciler
			.Select(k => new AvailableOgrenciDto(
				k.Id,
				k.Ad,
				k.Soyad,
				k.OdaNo,
				assignedSet.Contains(k.Id)
			))
			.ToList();
	}
}
