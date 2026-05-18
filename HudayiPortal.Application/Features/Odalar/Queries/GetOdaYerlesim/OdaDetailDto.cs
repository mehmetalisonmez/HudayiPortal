namespace HudayiPortal.Application.Features.Odalar.Queries.GetOdaYerlesim;

public sealed record OdaDetailDto(
	int Id,
	string OdaNo,
	int Kapasite,
	int Kat,
	int MevcutSayi,
	List<OdaOgrenciDto> Ogrenciler
);
