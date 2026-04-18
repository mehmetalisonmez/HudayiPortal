namespace HudayiPortal.Application.Features.Dashboard.Queries.GetOgrenciDashboard;

public sealed record OgrenciDashboardDto(
	string OdaNo,
	bool BugunYoklamaAlindiMi,
	int OkunmamisMesajSayisi,
	int YaklasanEtkinlikSayisi
);