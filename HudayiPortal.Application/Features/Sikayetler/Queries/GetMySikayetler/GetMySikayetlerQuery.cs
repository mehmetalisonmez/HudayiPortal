using HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetler;
using MediatR;

namespace HudayiPortal.Application.Features.Sikayetler.Queries.GetMySikayetler;

public sealed record GetMySikayetlerQuery() : IRequest<List<SikayetDto>>;
