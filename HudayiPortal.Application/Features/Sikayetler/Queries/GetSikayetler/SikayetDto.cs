namespace HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetler;

public sealed record SikayetDto(
	int Id,
	string OgrenciAdSoyad,
	string Baslik,
	string Icerik,
	string? Cevap,
	int? Durum,
	DateTime? OlusturulmaTarihi,
	DateTime? CevaplanmaTarihi
);
