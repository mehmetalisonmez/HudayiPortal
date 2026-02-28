using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class YemekYorum : Entity
{
	public Guid YemekMenuId { get; set; }
	public Guid KullaniciId { get; set; }

	public int Puan { get; set; } // 1-5 arası
	public string? Yorum { get; set; }

	public YemekMenu YemekMenu { get; set; }
	public Kullanici Kullanici { get; set; }
}
