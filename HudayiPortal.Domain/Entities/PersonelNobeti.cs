using HudayiPortal.Domain.Enums;

namespace HudayiPortal.Domain.Entities;

public class PersonelNobeti
{
	public int Id { get; set; }
	public int PersonelId { get; set; }
	public DateOnly Tarih { get; set; }
	public NobetTuru NobetTuru { get; set; }
	public string? Aciklama { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Kullanici Personel { get; set; } = null!;
}