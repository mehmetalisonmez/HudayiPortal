using MediatR;
using HudayiPortal.Application.Features.Auth.Queries.Login;

namespace HudayiPortal.Application.Features.Auth.Commands.VerifyOtp;

public sealed record VerifyOtpCommand(
	string Email,
	string OtpCode
) : IRequest<LoginResponseDto>;