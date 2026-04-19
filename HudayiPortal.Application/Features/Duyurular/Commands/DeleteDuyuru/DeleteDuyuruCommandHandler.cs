using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Commands.DeleteDuyuru;

public sealed class DeleteDuyuruCommandHandler : IRequestHandler<DeleteDuyuruCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteDuyuruCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteDuyuruCommand request, CancellationToken cancellationToken)
	{
		var duyuru = await _unitOfWork.Repository<Duyuru>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (duyuru is null)
			throw new KeyNotFoundException($"Duyuru bulunamadı: {request.Id}");

		// Soft Delete — fiziksel silme yerine bayrak ile işaretleme
		duyuru.SilindiMi = true;
		duyuru.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Duyuru>().Update(duyuru);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
