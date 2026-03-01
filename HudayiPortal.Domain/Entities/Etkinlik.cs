namespace HudayiPortal.Domain.Entities;

public class Etkinlik
{
	public int Id { get; set; }
	public string Baslik { get; set; } = null!;
	public string? Aciklama { get; set; }
	public DateTime BaslangicTarihi { get; set; }
	public DateTime? BitisTarihi { get; set; }
	public DateTime? SonKayitTarihi { get; set; }
	public decimal? Ucret { get; set; }
	public bool? ZorunluMu { get; set; }
	public string? ResimUrl { get; set; }
	public int? OlusturanPersonelId { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Kullanici? OlusturanPersonel { get; set; }
	public ICollection<EtkinlikKatilimcisi> EtkinlikKatilimcilari { get; set; } = new List<EtkinlikKatilimcisi>();
	public ICollection<EtkinlikYorumu> EtkinlikYorumlari { get; set; } = new List<EtkinlikYorumu>();
}
