using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class EtkinlikKatilimci : Entity
{
	public Guid EtkinlikId { get; set; }
	public Guid KullaniciId { get; set; }

	// Etkinlik bittikten sonra "Geldi/Gelmedi" işaretlemek için
	public bool? KatilimDurumu { get; set; }

	public Etkinlik Etkinlik { get; set; }
	public Kullanici Kullanici { get; set; }
}