using HudayiPortal.Application.Exceptions;
using HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetler;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Application.Features.Sikayetler.Queries.GetSikayetById;

public sealed class GetSikayetByIdQueryHandler
	: IRequestHandler<GetSikayetByIdQuery, SikayetDto?>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;

	public GetSikayetByIdQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
	}

	public async Task<SikayetDto?> Handle(GetSikayetByIdQuery request, CancellationToken cancellationToken)
	{
		var result = await _unitOfWork.Repository<Sikayet>()
			.Where(s => s.Id == request.Id && s.SilindiMi != true)
			.Include(s => s.GonderenKullanici)
			.FirstOrDefaultAsync(cancellationToken);

		if (result is null)
			return null;

		var currentUserId = _currentUserService.UserId;
		var roleName = _currentUserService.Role;

		if (roleName != "Admin" && roleName != "Personel" && result.GonderenKullaniciId != currentUserId)
		{
			throw new BusinessException("Bu işlem için yetkiniz bulunmamaktadır.");
		}

		return new SikayetDto(
			result.Id,
			$"{result.GonderenKullanici.Ad} {result.GonderenKullanici.Soyad}",
			result.Baslik,
			result.Icerik,
			result.Cevap,
			result.Durum,
			result.OlusturulmaTarihi,
			result.CevaplanmaTarihi);
	}
}
