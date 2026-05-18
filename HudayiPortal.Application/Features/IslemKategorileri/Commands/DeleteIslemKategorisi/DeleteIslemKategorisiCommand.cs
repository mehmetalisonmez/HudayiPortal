using MediatR;

namespace HudayiPortal.Application.Features.IslemKategorileri.Commands.DeleteIslemKategorisi;

public sealed record DeleteIslemKategorisiCommand(int Id) : IRequest;
