using MediatR;

namespace HudayiPortal.Application.Features.SohbetYoklamalar.Queries.GetSohbetYoklama;

public sealed record GetSohbetYoklamaQuery(
	DateOnly Tarih,
	int GrupId
) : IRequest<SohbetYoklamaResponse>;
