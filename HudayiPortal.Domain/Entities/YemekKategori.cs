using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class YemekKategori : Entity
{
	public string Ad { get; set; } // Çorba, Ana Yemek, Tatlı vb.
	public ICollection<YemekTanim> Yemekler { get; set; }
}