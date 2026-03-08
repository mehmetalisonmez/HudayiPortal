namespace HudayiPortal.Application.Features.Auth.Queries.Login;

public sealed record LoginResponse(
	int KullaniciId,
	string Ad,
	string Soyad,
	string Email,
	string Rol,
	string Token
);