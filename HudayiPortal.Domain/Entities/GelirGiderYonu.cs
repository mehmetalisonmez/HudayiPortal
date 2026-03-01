namespace HudayiPortal.Domain.Entities;

public class GelirGiderYonu
{
	public int Id { get; set; }
	public string YonAdi { get; set; } = null!;
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public ICollection<MaliIslem> MaliIslemler { get; set; } = new List<MaliIslem>();
}