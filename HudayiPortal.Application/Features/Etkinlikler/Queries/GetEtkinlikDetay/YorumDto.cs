namespace HudayiPortal.Application.Features.Etkinlikler.Queries.GetEtkinlikDetay;

public sealed record YorumDto(
	int Id,
	string YorumMetni,
	DateTime? OlusturulmaTarihi,
	string KullaniciAdSoyad
);
