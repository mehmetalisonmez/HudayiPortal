using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Kullanicilar.Commands.DeleteKullanici;

public sealed class DeleteKullaniciCommandHandler : IRequestHandler<DeleteKullaniciCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteKullaniciCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteKullaniciCommand request, CancellationToken cancellationToken)
	{
		var kullanici = await _unitOfWork.Repository<Kullanici>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (kullanici is null)
			throw new KeyNotFoundException($"Kullanıcı bulunamadı: {request.Id}");

		// Soft Delete — fiziksel silme yerine bayrak ile işaretleme
		kullanici.SilindiMi = true;
		kullanici.AktifMi = false;
		kullanici.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Kullanici>().Update(kullanici);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
