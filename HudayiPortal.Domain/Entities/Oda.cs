namespace HudayiPortal.Domain.Entities;

public class Oda
{
	public int Id { get; set; }
	public string OdaNo { get; set; } = null!;
	public int Kapasite { get; set; }
	public int Kat { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public ICollection<Kullanici> Kullanicilar { get; set; } = new List<Kullanici>();
}