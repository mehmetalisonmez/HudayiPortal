using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class Rol : Entity
{
	public string RolAdi { get; set; } // "Öğrenci", "Yönetici", "Belletmen"

	// Navigation Property (Bir rolün birden çok kullanıcısı olabilir)
	public ICollection<Kullanici> Kullanicilar { get; set; }
}