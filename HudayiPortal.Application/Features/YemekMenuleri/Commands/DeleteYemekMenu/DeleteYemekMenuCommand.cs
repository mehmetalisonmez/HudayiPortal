using MediatR;

namespace HudayiPortal.Application.Features.YemekMenuleri.Commands.DeleteYemekMenu;

public sealed record DeleteYemekMenuCommand(int Id) : IRequest<Unit>;
