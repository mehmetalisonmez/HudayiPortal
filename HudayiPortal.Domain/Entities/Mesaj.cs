namespace HudayiPortal.Domain.Entities;

public class Mesaj
{
	public int Id { get; set; }
	public int GonderenId { get; set; }
	public int? AliciId { get; set; }
	public int? ChatGrupId { get; set; }
	public string MesajIcerigi { get; set; } = null!;
	public bool? OkunduMu { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Kullanici Gonderen { get; set; } = null!;
	public Kullanici? Alici { get; set; }
	public ChatGrubu? ChatGrup { get; set; }
}