using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;

namespace HudayiPortal.Application.Features.Mesajlar.Commands.SendMessage;

public sealed class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, int>
{
	private readonly IUnitOfWork _unitOfWork;

	public SendMessageCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<int> Handle(SendMessageCommand request, CancellationToken cancellationToken)
	{
		var mesaj = new Mesaj
		{
			GonderenId = 1,
			AliciId = request.AliciId,
			ChatGrupId = request.ChatGrupId,
			MesajIcerigi = request.MesajIcerigi,
			OkunduMu = false,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<Mesaj>().AddAsync(mesaj, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return mesaj.Id;
	}
}
