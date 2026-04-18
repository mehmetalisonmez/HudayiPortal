namespace HudayiPortal.Application.Features.Dashboard.Queries.GetYoneticiDashboard;

public sealed record YoneticiDashboardDto(
	int ToplamOgrenciSayisi,
	int BekleyenSikayetSayisi,
	int AktifDuyuruSayisi,
	int YaklasanEtkinlikSayisi
);