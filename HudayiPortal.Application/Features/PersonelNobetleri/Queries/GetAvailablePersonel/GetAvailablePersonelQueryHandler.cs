using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.PersonelNobetleri.Queries.GetAvailablePersonel;

public sealed class GetAvailablePersonelQueryHandler : IRequestHandler<GetAvailablePersonelQuery, List<AvailablePersonelDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAvailablePersonelQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<AvailablePersonelDto>> Handle(GetAvailablePersonelQuery request, CancellationToken cancellationToken)
	{
		return await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.Rol.RolAdi == "Personel"
					 && k.AktifMi == true
					 && k.SilindiMi != true)
			.Include(k => k.Rol)
			.OrderBy(k => k.Ad)
			.ThenBy(k => k.Soyad)
			.Select(k => new AvailablePersonelDto(k.Id, k.Ad, k.Soyad))
			.ToListAsync(cancellationToken);
	}
}
