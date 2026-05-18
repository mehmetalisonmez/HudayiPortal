using HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetler;
using MediatR;

namespace HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetById;

public sealed record GetSikayetByIdQuery(int Id) : IRequest<SikayetDto?>;
