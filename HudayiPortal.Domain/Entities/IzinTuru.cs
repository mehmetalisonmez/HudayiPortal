namespace HudayiPortal.Domain.Entities;

public class IzinTuru
{
	public int Id { get; set; }
	public string TurAdi { get; set; } = null!;
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public ICollection<Izin> Izinler { get; set; } = new List<Izin>();
}
