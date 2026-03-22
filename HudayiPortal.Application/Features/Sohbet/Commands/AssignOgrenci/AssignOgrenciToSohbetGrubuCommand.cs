using MediatR;

namespace HudayiPortal.Application.Features.Sohbet.Commands.AssignOgrenci;

public sealed record AssignOgrenciToSohbetGrubuCommand(
	int KullaniciId,
	int SohbetGrupId
) : IRequest<int>;
