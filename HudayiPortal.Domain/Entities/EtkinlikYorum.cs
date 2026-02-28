using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class EtkinlikYorum : Entity
{
	public Guid EtkinlikId { get; set; }
	public Guid KullaniciId { get; set; }

	public string YorumMetni { get; set; }
	public int Puan { get; set; } // Memnuniyet puanı (1-5 arası)

	// Navigation Properties
	public Etkinlik Etkinlik { get; set; }
	public Kullanici Kullanici { get; set; }
}