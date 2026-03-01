namespace HudayiPortal.Domain.Entities;

public class SohbetYoklama
{
	public int Id { get; set; }
	public int SohbetId { get; set; }
	public int KullaniciId { get; set; }
	public bool KatilimDurumu { get; set; }
	public string? MazeretAciklamasi { get; set; }
	public int? YoklamayiAlanPersonelId { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Sohbet Sohbet { get; set; } = null!;
	public Kullanici Kullanici { get; set; } = null!;
	public Kullanici? YoklamayiAlanPersonel { get; set; }
}
