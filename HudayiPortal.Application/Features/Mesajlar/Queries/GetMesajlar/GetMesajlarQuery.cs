using MediatR;

namespace HudayiPortal.Application.Features.Mesajlar.Queries.GetMesajlar;

public sealed record GetMesajlarQuery(
	int? AliciId,
	int? ChatGrupId
) : IRequest<List<MesajDto>>;
