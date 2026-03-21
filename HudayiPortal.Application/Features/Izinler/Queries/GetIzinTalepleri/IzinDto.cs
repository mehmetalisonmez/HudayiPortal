namespace HudayiPortal.Application.Features.Izinler.Queries.GetIzinTalepleri;

public sealed record IzinDto(
	int Id,
	string OgrenciAdSoyad,
	string IzinTuruAdi,
	DateTime BaslangicTarihi,
	DateTime BitisTarihi,
	string GidecegiAdres,
	int OnayDurumu
);
