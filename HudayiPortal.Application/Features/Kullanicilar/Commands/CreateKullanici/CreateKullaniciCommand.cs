using MediatR;

namespace HudayiPortal.Application.Features.Kullanicilar.Commands.CreateKullanici;

public sealed record CreateKullaniciCommand(
	int RolId,
	int? OdaId,
	string Ad,
	string Soyad,
	string? TcKimlikNo,
	string? Telefon,
	string? Email,
	string? Sifre,
	DateTime? DogumTarihi,
	string? KanGrubu
) : IRequest<int>;