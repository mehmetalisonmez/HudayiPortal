using MediatR;

namespace HudayiPortal.Application.Features.Mesajlar.Queries.GetMesajGecmisi;

public sealed record GetMesajGecmisiQuery(
	int? AliciId,
	int? ChatGrupId
) : IRequest<List<MesajDto>>;
