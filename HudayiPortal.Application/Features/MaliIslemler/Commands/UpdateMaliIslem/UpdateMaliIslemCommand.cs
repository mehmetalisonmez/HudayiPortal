using MediatR;

namespace HudayiPortal.Application.Features.MaliIslemler.Commands.UpdateMaliIslem;

public sealed record UpdateMaliIslemCommand(
	int Id,
	int YonId,
	string Baslik,
	string? Aciklama,
	decimal Tutar,
	DateTime IslemTarihi,
	int? IlgiliKullaniciId,
	int? KategoriId,
	string? BelgeUrl
) : IRequest;
