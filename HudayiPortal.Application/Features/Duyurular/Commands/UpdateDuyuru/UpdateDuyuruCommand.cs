using MediatR;

namespace HudayiPortal.Application.Features.Duyurular.Commands.UpdateDuyuru;

public sealed record UpdateDuyuruCommand(
	int Id,
	string Baslik,
	string Icerik,
	DateTime? YayinTarihi,
	DateTime? GecerlilikTarihi,
	int? HedefRolId
) : IRequest<Unit>;
