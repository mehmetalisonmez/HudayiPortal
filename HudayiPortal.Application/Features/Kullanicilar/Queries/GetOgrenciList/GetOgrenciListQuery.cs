using HudayiPortal.Application.Wrappers;
using MediatR;

namespace HudayiPortal.Application.Features.Kullanicilar.Queries.GetOgrenciList;

public sealed record GetOgrenciListQuery(
	int PageNumber = 1,
	int PageSize = 10,
	string? SearchTerm = null
) : IRequest<PagedResponse<KullaniciListDto>>;