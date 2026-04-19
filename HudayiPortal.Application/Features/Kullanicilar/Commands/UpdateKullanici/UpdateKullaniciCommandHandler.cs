using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Kullanicilar.Commands.UpdateKullanici;

public sealed class UpdateKullaniciCommandHandler : IRequestHandler<UpdateKullaniciCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateKullaniciCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdateKullaniciCommand request, CancellationToken cancellationToken)
	{
		var kullanici = await _unitOfWork.Repository<Kullanici>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (kullanici is null)
			throw new KeyNotFoundException($"Kullanıcı bulunamadı: {request.Id}");

		// Alanları güncelle
		kullanici.OdaId = request.OdaId;
		kullanici.Ad = request.Ad;
		kullanici.Soyad = request.Soyad;
		kullanici.TcKimlikNo = request.TcKimlikNo;
		kullanici.Telefon = request.Telefon;
		kullanici.Email = request.Email;
		kullanici.DogumTarihi = request.DogumTarihi;
		kullanici.KanGrubu = request.KanGrubu;
		kullanici.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Kullanici>().Update(kullanici);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
