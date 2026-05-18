using MediatR;

namespace HudayiPortal.Application.Features.MaliIslemler.Commands.DeleteMaliIslem;

public sealed record DeleteMaliIslemCommand(int Id) : IRequest;
