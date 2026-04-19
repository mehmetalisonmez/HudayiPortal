using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Izinler.Commands.DeleteIzinTalebi;

public sealed class DeleteIzinTalebiCommandHandler : IRequestHandler<DeleteIzinTalebiCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteIzinTalebiCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteIzinTalebiCommand request, CancellationToken cancellationToken)
	{
		var izin = await _unitOfWork.Repository<Izin>()
			.GetByIdAsync(request.Id, cancellationToken);

		if (izin is null)
			throw new KeyNotFoundException($"İzin talebi bulunamadı: {request.Id}");

		// Soft Delete — fiziksel silme yerine bayrak ile işaretleme
		izin.SilindiMi = true;
		izin.GuncellenmeTarihi = DateTime.UtcNow;

		_unitOfWork.Repository<Izin>().Update(izin);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
