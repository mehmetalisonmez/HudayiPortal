using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Roller.Queries.GetRolList;

public sealed class GetRolListQueryHandler : IRequestHandler<GetRolListQuery, List<RolDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetRolListQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<RolDto>> Handle(GetRolListQuery request, CancellationToken cancellationToken)
	{
		var roller = await _unitOfWork.Repository<Rol>()
			.Where(r => r.SilindiMi == false)
			.OrderBy(r => r.RolAdi)
			.Select(r => new RolDto(r.Id, r.RolAdi))
			.ToListAsync(cancellationToken);

		return roller;
	}
}
