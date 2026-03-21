namespace HudayiPortal.Domain.Entities;

public class Izin
{
	public int Id { get; set; }
	public int KullaniciId { get; set; }
	public int IzinTurId { get; set; }
	public DateTime BaslangicTarihi { get; set; }
	public DateTime BitisTarihi { get; set; }
	public string GidecegiAdres { get; set; } = null!;
	public string Aciklama { get; set; } = null!;
	public int OnayDurumu { get; set; }
	public int? OnaylayanPersonelId { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Kullanici Kullanici { get; set; } = null!;
	public IzinTuru IzinTuru { get; set; } = null!;
	public Kullanici? OnaylayanPersonel { get; set; }
}
