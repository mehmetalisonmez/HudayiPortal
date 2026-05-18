namespace HudayiPortal.Application.Features.MaliIslemler.Queries.GetFinansDashboard;

public sealed record FinansDashboardDto(
	decimal ToplamGelir,
	decimal ToplamGider,
	decimal NetKasa,
	List<KategoriDagilimiDto> KategoriDagilimi,
	List<AylikTrendDto> AylikTrend
);

public sealed record KategoriDagilimiDto(
	string KategoriAdi,
	decimal Tutar,
	double Yuzde
);

public sealed record AylikTrendDto(
	string Ay,
	decimal Gelir,
	decimal Gider
);
