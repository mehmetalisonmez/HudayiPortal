using MediatR;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetSohbetGruplari;

public sealed record GetSohbetGruplariQuery : IRequest<List<SohbetGrubuDto>>;
