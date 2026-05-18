using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.SyncOgrenciler;

public sealed record SyncOgrencilerCommand(
	int SohbetGrupId,
	List<int> KullaniciIds
) : IRequest<Unit>;
