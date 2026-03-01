namespace HudayiPortal.Domain.Entities;

public class Rol
{
	public int Id { get; set; }
	public string RolAdi { get; set; } = null!;
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public ICollection<Kullanici> Kullanicilar { get; set; } = new List<Kullanici>();
	public ICollection<Duyuru> Duyurular { get; set; } = new List<Duyuru>();
}