using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.Odalar.Commands.AssignStudentToRoom;

public sealed class AssignStudentToRoomCommandHandler
	: IRequestHandler<AssignStudentToRoomCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public AssignStudentToRoomCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(AssignStudentToRoomCommand request, CancellationToken cancellationToken)
	{
		var kullanici = await _unitOfWork.Repository<Kullanici>()
			.GetByIdAsync(request.KullaniciId, cancellationToken);

		if (kullanici is null || kullanici.SilindiMi == true)
			throw new KeyNotFoundException($"Kullanıcı bulunamadı: {request.KullaniciId}");

		// Kapasite kontrolü
		if (request.OdaId.HasValue)
		{
			var oda = await _unitOfWork.Repository<Oda>()
				.GetByIdAsync(request.OdaId.Value, cancellationToken);

			if (oda is null || oda.SilindiMi == true)
				throw new KeyNotFoundException($"Oda bulunamadı: {request.OdaId.Value}");

			var mevcutSayi = await _unitOfWork.Repository<Kullanici>()
				.Where(k => k.OdaId == request.OdaId.Value
					&& k.SilindiMi != true
					&& k.AktifMi == true
					&& k.Id != request.KullaniciId) // Aynı odaya tekrar atanıyorsa sayma
				.CountAsync(cancellationToken);

			if (mevcutSayi >= oda.Kapasite)
				throw new Application.Exceptions.ValidationException(
					"Bu oda kapasitesi dolu.",
					new List<string> { "Bu oda kapasitesi dolu." });
		}

		kullanici.OdaId = request.OdaId;
		kullanici.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Kullanici>().Update(kullanici);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
