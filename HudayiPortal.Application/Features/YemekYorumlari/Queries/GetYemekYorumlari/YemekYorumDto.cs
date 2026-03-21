namespace HudayiPortal.Application.Features.YemekYorumlari.Queries.GetYemekYorumlari;

public sealed record YemekYorumDto(
	int Id,
	int YemekMenuId,
	int KullaniciId,
	string AdSoyad,
	string YorumMetni,
	int? Puan,
	DateTime? OlusturulmaTarihi
);
