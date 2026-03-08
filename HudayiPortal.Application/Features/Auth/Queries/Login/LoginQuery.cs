using MediatR;

namespace HudayiPortal.Application.Features.Auth.Queries.Login;

public sealed record LoginQuery(
	string Email,
	string Sifre
) : IRequest<LoginResponse>;