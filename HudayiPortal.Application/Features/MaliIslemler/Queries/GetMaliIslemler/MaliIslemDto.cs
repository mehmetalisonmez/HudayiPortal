namespace HudayiPortal.Application.Features.MaliIslemler.Queries.GetMaliIslemler;

public sealed record MaliIslemDto(
	int Id,
	string YonAdi,
	string Baslik,
	string? Aciklama,
	decimal Tutar,
	DateTime IslemTarihi,
	string? IlgiliKullaniciAdSoyad
);
