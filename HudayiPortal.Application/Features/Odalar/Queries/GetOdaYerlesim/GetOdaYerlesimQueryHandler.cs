using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Odalar.Queries.GetOdaYerlesim;

public sealed class GetOdaYerlesimQueryHandler
	: IRequestHandler<GetOdaYerlesimQuery, OdaYerlesimResultDto>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetOdaYerlesimQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<OdaYerlesimResultDto> Handle(GetOdaYerlesimQuery request, CancellationToken cancellationToken)
	{
		// Tüm odalar + aktif öğrencileri
		var odalar = await _unitOfWork.Repository<Oda>()
			.Where(o => o.SilindiMi != true)
			.OrderBy(o => o.Kat)
			.ThenBy(o => o.OdaNo)
			.Select(o => new OdaDetailDto(
				o.Id,
				o.OdaNo,
				o.Kapasite,
				o.Kat,
				o.Kullanicilar.Count(k => k.SilindiMi != true && k.AktifMi == true),
				o.Kullanicilar
					.Where(k => k.SilindiMi != true && k.AktifMi == true)
					.Select(k => new OdaOgrenciDto(k.Id, k.Ad, k.Soyad, k.Telefon))
					.ToList()))
			.ToListAsync(cancellationToken);

		// Odasız öğrenciler (RolId = 3 → Öğrenci varsayımı yerine Rol adı ile kontrol)
		var odasizOgrenciler = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.OdaId == null
				&& k.SilindiMi != true
				&& k.AktifMi == true
				&& k.Rol.RolAdi == "Ogrenci")
			.OrderBy(k => k.Ad)
			.ThenBy(k => k.Soyad)
			.Select(k => new OdasizOgrenciDto(k.Id, k.Ad, k.Soyad, k.Telefon))
			.ToListAsync(cancellationToken);

		return new OdaYerlesimResultDto(odalar, odasizOgrenciler);
	}
}
