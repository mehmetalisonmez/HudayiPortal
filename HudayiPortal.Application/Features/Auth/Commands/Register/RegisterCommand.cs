using MediatR;

namespace HudayiPortal.Application.Features.Auth.Commands.Register;

public sealed record RegisterCommand(
	string Ad,
	string Soyad,
	string TcKimlikNo,
	string Telefon,
	string Email,
	string Sifre
) : IRequest<int>;
