using MediatR;

namespace HudayiPortal.Application.Features.Yoklamalar.Queries.GetOgrencilerForYoklama;

public sealed record GetOgrencilerForYoklamaQuery : IRequest<List<OgrenciYoklamaDto>>;
