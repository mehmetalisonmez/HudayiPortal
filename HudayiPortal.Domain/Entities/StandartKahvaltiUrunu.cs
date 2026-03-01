namespace HudayiPortal.Domain.Entities;

public class StandartKahvaltiUrunu
{
	public int Id { get; set; }
	public int YemekTanimId { get; set; }
	public bool? AktifMi { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public YemekTanimi YemekTanim { get; set; } = null!;
}