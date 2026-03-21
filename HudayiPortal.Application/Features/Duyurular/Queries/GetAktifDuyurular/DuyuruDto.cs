namespace HudayiPortal.Application.Features.Duyurular.Queries.GetAktifDuyurular;

public sealed record DuyuruDto(
	int Id,
	string Baslik,
	string Icerik,
	DateTime? GecerlilikTarihi,
	DateTime? OlusturulmaTarihi
);
