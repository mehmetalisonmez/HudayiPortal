using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Dashboard.Queries.GetOgrenciDashboard;

public sealed class GetOgrenciDashboardQueryHandler : IRequestHandler<GetOgrenciDashboardQuery, OgrenciDashboardDto>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public GetOgrenciDashboardQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<OgrenciDashboardDto> Handle(GetOgrenciDashboardQuery request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId;

		var kullanici = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.Id == userId)
			.Include(k => k.Oda)
			.FirstOrDefaultAsync(cancellationToken);

		var odaNo = kullanici?.Oda?.OdaNo ?? "Atanmadý";

		var bugun = DateOnly.FromDateTime(DateTime.UtcNow);
		var yoklamaAlindiMi = await _unitOfWork.Repository<GunlukYoklama>()
			.AnyAsync(y => y.KullaniciId == userId && y.Tarih == bugun && y.SilindiMi != true, cancellationToken);

		var mesajSayisi = await _unitOfWork.Repository<Mesaj>()
			.Where(m => m.AliciId == userId && m.OkunduMu != true && m.SilindiMi != true)
			.CountAsync(cancellationToken);

		var etkinlikSayisi = await _unitOfWork.Repository<Etkinlik>()
			.Where(e => e.BaslangicTarihi >= DateTime.UtcNow && e.SilindiMi != true)
			.CountAsync(cancellationToken);

		return new OgrenciDashboardDto(
			OdaNo: odaNo,
			BugunYoklamaAlindiMi: yoklamaAlindiMi,
			OkunmamisMesajSayisi: mesajSayisi,
			YaklasanEtkinlikSayisi: etkinlikSayisi
		);
	}
}