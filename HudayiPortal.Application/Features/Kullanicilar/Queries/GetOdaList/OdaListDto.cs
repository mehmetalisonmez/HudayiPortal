namespace HudayiPortal.Application.Features.Kullanicilar.Queries.GetOdaList;

public sealed record OdaListDto(
	int Id,
	string OdaNo,
	int Kat,
	int Kapasite
);
