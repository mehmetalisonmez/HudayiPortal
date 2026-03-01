namespace HudayiPortal.Domain.Entities;

public class ChatGrupUyesi
{
	public int Id { get; set; }
	public int ChatGrupId { get; set; }
	public int KullaniciId { get; set; }
	public bool? IsAdmin { get; set; }
	public DateTime? KatilmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public ChatGrubu ChatGrup { get; set; } = null!;
	public Kullanici Kullanici { get; set; } = null!;
}