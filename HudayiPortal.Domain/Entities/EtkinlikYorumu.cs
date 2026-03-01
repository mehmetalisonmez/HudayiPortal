namespace HudayiPortal.Domain.Entities;

public class EtkinlikYorumu
{
	public int Id { get; set; }
	public int EtkinlikId { get; set; }
	public int KullaniciId { get; set; }
	public string YorumMetni { get; set; } = null!;
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Etkinlik Etkinlik { get; set; } = null!;
	public Kullanici Kullanici { get; set; } = null!;
}