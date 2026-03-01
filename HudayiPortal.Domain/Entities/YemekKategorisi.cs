namespace HudayiPortal.Domain.Entities;

public class YemekKategorisi
{
	public int Id { get; set; }
	public string KategoriAdi { get; set; } = null!;
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public ICollection<YemekTanimi> YemekTanimlari { get; set; } = new List<YemekTanimi>();
}