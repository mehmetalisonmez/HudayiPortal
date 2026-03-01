namespace HudayiPortal.Domain.Entities;

public class YemekYorumu
{
	public int Id { get; set; }
	public int YemekMenuId { get; set; }
	public int KullaniciId { get; set; }
	public string YorumMetni { get; set; } = null!;
	public int? Puan { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public YemekMenusu YemekMenu { get; set; } = null!;
	public Kullanici Kullanici { get; set; } = null!;
}