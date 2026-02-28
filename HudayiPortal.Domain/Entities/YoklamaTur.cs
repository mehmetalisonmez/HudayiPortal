using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class YoklamaTur : Entity
{
	public string Ad { get; set; } // "Sabah Namazı", "Gece Yoklaması"
	public ICollection<GunlukYoklama> Yoklamalar { get; set; }
}