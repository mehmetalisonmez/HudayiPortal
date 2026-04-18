using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Dashboard.Queries.GetYoneticiDashboard;

public sealed class GetYoneticiDashboardQueryHandler : IRequestHandler<GetYoneticiDashboardQuery, YoneticiDashboardDto>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetYoneticiDashboardQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<YoneticiDashboardDto> Handle(GetYoneticiDashboardQuery request, CancellationToken cancellationToken)
	{
		// EF Core does not support multiple parallel async operations on the same DbContext instance
		// Therefore, we await them sequentially.
		var ogrenciSayisi = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.RolId == 1 && k.SilindiMi != true)
			.CountAsync(cancellationToken);

		var sikayetSayisi = await _unitOfWork.Repository<Sikayet>()
			.Where(s => s.Durum == 0 && s.SilindiMi != true)
			.CountAsync(cancellationToken);

		var duyuruSayisi = await _unitOfWork.Repository<Duyuru>()
			.Where(d => d.SilindiMi != true)
			.CountAsync(cancellationToken);

		var etkinlikSayisi = await _unitOfWork.Repository<Etkinlik>()
			.Where(e => e.BaslangicTarihi >= DateTime.UtcNow && e.SilindiMi != true)
			.CountAsync(cancellationToken);

		return new YoneticiDashboardDto(
			ToplamOgrenciSayisi: ogrenciSayisi,
			BekleyenSikayetSayisi: sikayetSayisi,
			AktifDuyuruSayisi: duyuruSayisi,
			YaklasanEtkinlikSayisi: etkinlikSayisi
		);
	}
}