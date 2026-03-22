using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.AssignOgrenci;

public sealed class AssignOgrenciToSohbetGrubuCommandHandler : IRequestHandler<AssignOgrenciToSohbetGrubuCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public AssignOgrenciToSohbetGrubuCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(AssignOgrenciToSohbetGrubuCommand request, CancellationToken cancellationToken)
	{
		var atama = new OgrenciSohbetGrubu
		{
			KullaniciId = request.KullaniciId,
			SohbetGrupId = request.SohbetGrupId,
			AtanmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<OgrenciSohbetGrubu>().AddAsync(atama, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return atama.Id;
	}
}
