using MediatR;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetYoklamaByDateAndType;

public sealed record GetYoklamaByDateAndTypeQuery(
	DateOnly Tarih,
	int YoklamaTurId
) : IRequest<List<OgrenciYoklamaDurumDto>>;
