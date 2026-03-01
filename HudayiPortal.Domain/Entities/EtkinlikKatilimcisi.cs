namespace HudayiPortal.Domain.Entities;

public class EtkinlikKatilimcisi
{
	public int Id { get; set; }
	public int EtkinlikId { get; set; }
	public int KullaniciId { get; set; }
	public bool? KatilimDurumu { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Etkinlik Etkinlik { get; set; } = null!;
	public Kullanici Kullanici { get; set; } = null!;
}