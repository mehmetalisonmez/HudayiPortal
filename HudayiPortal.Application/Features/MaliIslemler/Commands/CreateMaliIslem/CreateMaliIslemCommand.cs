using MediatR;

namespace HudayiPortal.Application.Features.MaliIslemler.Commands.CreateMaliIslem;

public sealed record CreateMaliIslemCommand(
	int YonId,
	string Baslik,
	string Aciklama,
	decimal Tutar,
	DateTime IslemTarihi,
	int? IlgiliKullaniciId
) : IRequest<int>;
