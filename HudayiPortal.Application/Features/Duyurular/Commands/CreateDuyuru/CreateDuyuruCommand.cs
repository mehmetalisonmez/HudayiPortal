using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Commands.CreateDuyuru;

public sealed record CreateDuyuruCommand(
	string Baslik,
	string Icerik,
	DateTime? GecerlilikTarihi,
	int? HedefRolId
) : IRequest<int>;
