using MediatR;

namespace HudayiPortal.Application.Features.YemekTanimlari.Commands.DeleteYemekTanimi;

public sealed record DeleteYemekTanimiCommand(int Id) : IRequest<Unit>;
