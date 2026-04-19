using MediatR;

namespace HudayiPortal.Application.Features.Kullanicilar.Commands.UpdateKullanici;

public sealed record UpdateKullaniciCommand(
	int Id,
	int? OdaId,
	string Ad,
	string Soyad,
	string? TcKimlikNo,
	string? Telefon,
	string? Email,
	DateTime? DogumTarihi,
	string? KanGrubu
) : IRequest<Unit>;
