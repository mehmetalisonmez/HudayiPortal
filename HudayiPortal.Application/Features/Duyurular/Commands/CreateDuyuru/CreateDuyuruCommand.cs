using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Commands.CreateDuyuru;

public sealed record CreateDuyuruCommand(
	string Baslik,
	string Icerik,
	DateTime? YayinTarihi,
	DateTime? GecerlilikTarihi,
	int? HedefRolId
) : IRequest<int>;
