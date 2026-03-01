namespace HudayiPortal.Domain.Entities;

public class Sikayet
{
	public int Id { get; set; }
	public int GonderenKullaniciId { get; set; }
	public string Baslik { get; set; } = null!;
	public string Icerik { get; set; } = null!;
	public string? Cevap { get; set; }
	public int? Durum { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? CevaplanmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Kullanici GonderenKullanici { get; set; } = null!;
}