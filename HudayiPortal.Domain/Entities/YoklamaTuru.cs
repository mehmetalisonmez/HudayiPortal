namespace HudayiPortal.Domain.Entities;

public class YoklamaTuru
{
	public int Id { get; set; }
	public string TurAdi { get; set; } = null!;
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public ICollection<GunlukYoklama> GunlukYoklamalar { get; set; } = new List<GunlukYoklama>();
}