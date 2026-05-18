namespace HudayiPortal.Domain.Entities;

public class IslemKategorileri
{
	public int Id { get; set; }
	public string KategoriAdi { get; set; } = null!;
	public int YonId { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public GelirGiderYonu Yon { get; set; } = null!;
	public ICollection<MaliIslem> MaliIslemler { get; set; } = new List<MaliIslem>();
}
