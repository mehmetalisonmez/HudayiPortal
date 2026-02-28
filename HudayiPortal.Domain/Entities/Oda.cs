using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class Oda : Entity
{
	public string OdaNo { get; set; } // Örn: "A-101"
	public int Kapasite { get; set; }
	public int Kat { get; set; }

	// Navigation Property (Bir odada birden çok öğrenci kalabilir)
	public ICollection<Kullanici> Kullanicilar { get; set; }
}