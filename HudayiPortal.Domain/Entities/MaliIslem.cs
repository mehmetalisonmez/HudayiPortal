namespace HudayiPortal.Domain.Entities;

public class MaliIslem
{
	public int Id { get; set; }
	public int YonId { get; set; }
	public string Baslik { get; set; } = null!;
	public string? Aciklama { get; set; }
	public decimal Tutar { get; set; }
	public DateTime IslemTarihi { get; set; }
	public int? IlgiliKullaniciId { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public GelirGiderYonu Yon { get; set; } = null!;
	public Kullanici? IlgiliKullanici { get; set; }
}
