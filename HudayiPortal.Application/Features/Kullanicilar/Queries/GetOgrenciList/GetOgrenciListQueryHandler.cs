using HudayiPortal.Application.Wrappers;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Kullanicilar.Queries.GetOgrenciList;

public sealed class GetOgrenciListQueryHandler
	: IRequestHandler<GetOgrenciListQuery, PagedResponse<KullaniciListDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetOgrenciListQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<PagedResponse<KullaniciListDto>> Handle(
		GetOgrenciListQuery request,
		CancellationToken cancellationToken)
	{
		// Base query: active, non-deleted students (Rol = "Ogrenci")
		var query = _unitOfWork.Repository<Kullanici>()
			.Where(k => k.SilindiMi != true
						&& k.AktifMi == true
						&& k.Rol.RolAdi == "Ogrenci")
			.Include(k => k.Oda)
			.Include(k => k.Rol)
			.AsQueryable();

		// Apply search filter on Ad, Soyad, or TcKimlikNo
		if (!string.IsNullOrWhiteSpace(request.SearchTerm))
		{
			var term = request.SearchTerm.Trim().ToLower();
			query = query.Where(k =>
				k.Ad.ToLower().Contains(term) ||
				k.Soyad.ToLower().Contains(term) ||
				(k.TcKimlikNo != null && k.TcKimlikNo.Contains(term)));
		}

		// Get total count before pagination
		var totalRecords = await query.CountAsync(cancellationToken);

		// Apply pagination and project to DTO
		var data = await query
			.OrderBy(k => k.Soyad)
			.ThenBy(k => k.Ad)
			.Skip((request.PageNumber - 1) * request.PageSize)
			.Take(request.PageSize)
			.Select(k => new KullaniciListDto(
				k.Id,
				k.Ad,
				k.Soyad,
				k.TcKimlikNo,
				k.Telefon,
				k.Email,
				k.Oda != null ? k.Oda.OdaNo : null,
				k.Oda != null ? k.Oda.Kat : null,
				k.AktifMi))
			.ToListAsync(cancellationToken);

		return new PagedResponse<KullaniciListDto>(
			data,
			totalRecords,
			request.PageNumber,
			request.PageSize);
	}
}