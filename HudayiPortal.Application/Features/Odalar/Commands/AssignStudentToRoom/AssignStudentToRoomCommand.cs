using MediatR;

namespace HudayiPortal.Application.Features.Odalar.Commands.AssignStudentToRoom;

public sealed record AssignStudentToRoomCommand(
	int KullaniciId,
	int? OdaId
) : IRequest<Unit>;
