using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class GelirGiderYon : Entity
{
	public string Ad { get; set; } // "Gelir", "Gider"
	public ICollection<MaliIslem> Islemler { get; set; }
}