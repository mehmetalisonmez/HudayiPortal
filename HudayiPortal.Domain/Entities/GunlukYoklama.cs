namespace HudayiPortal.Domain.Entities;

public sealed class GunlukYoklama
{
	public int Id { get; set; }
	public int KullaniciId { get; set; }
	public int YoklamaTurId { get; set; }
	public DateOnly Tarih { get; set; }
	public bool Durum { get; set; }
	public string? Aciklama { get; set; }
	public int? YoklamayiAlanPersonelId { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Kullanici Kullanici { get; set; } = null!;
	public YoklamaTuru YoklamaTur { get; set; } = null!;
	public Kullanici? YoklamayiAlanPersonel { get; set; }
}