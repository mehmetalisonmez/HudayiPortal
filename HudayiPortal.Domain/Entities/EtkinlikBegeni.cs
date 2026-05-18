namespace HudayiPortal.Domain.Entities;

public class EtkinlikBegeni
{
	public int Id { get; set; }
	public int EtkinlikId { get; set; }
	public int KullaniciId { get; set; }
	public DateTime OlusturulmaTarihi { get; set; }

	// Navigation properties
	public Etkinlik Etkinlik { get; set; } = null!;
	public Kullanici Kullanici { get; set; } = null!;
}
