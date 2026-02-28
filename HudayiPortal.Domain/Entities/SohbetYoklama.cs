using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class SohbetYoklama : Entity
{
	public Guid SohbetId { get; set; }
	public Guid KullaniciId { get; set; }

	public bool KatildiMi { get; set; } // Var/Yok
	public string? Mazeret { get; set; }

	public Sohbet Sohbet { get; set; }
	public Kullanici Ogrenci { get; set; }
}
