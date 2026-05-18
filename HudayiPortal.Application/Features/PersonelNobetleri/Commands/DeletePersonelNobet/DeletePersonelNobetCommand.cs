using MediatR;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Commands.DeletePersonelNobet;

public sealed record DeletePersonelNobetCommand(int Id) : IRequest<Unit>;
