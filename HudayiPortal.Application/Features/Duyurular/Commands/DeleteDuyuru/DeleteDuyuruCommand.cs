using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Commands.DeleteDuyuru;

public sealed record DeleteDuyuruCommand(int Id) : IRequest<Unit>;
