using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Queries.GetAllSohbetGruplari;

public sealed record GetAllSohbetGruplariQuery : IRequest<List<SohbetGrubuDetailDto>>;
