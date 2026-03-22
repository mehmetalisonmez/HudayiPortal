using MediatR;

namespace HudayiPortal.Application.Features.Auth.Commands.VerifyEmail;

public sealed record VerifyEmailCommand(string Token) : IRequest<bool>;
