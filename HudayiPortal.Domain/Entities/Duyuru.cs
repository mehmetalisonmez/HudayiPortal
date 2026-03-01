namespace HudayiPortal.Domain.Entities;

public class Duyuru
{
	public int Id { get; set; }
	public string Baslik { get; set; } = null!;
	public string Icerik { get; set; } = null!;
	public int? HedefRolId { get; set; }
	public DateTime? YayinTarihi { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Rol? HedefRol { get; set; }
}