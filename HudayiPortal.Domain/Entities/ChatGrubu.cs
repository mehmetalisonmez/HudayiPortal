namespace HudayiPortal.Domain.Entities;

public class ChatGrubu
{
	public int Id { get; set; }
	public string GrupAdi { get; set; } = null!;
	public string? GrupResmiUrl { get; set; }
	public int? OlusturanKullaniciId { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Kullanici? OlusturanKullanici { get; set; }
	public ICollection<ChatGrupUyesi> ChatGrupUyeleri { get; set; } = new List<ChatGrupUyesi>();
	public ICollection<Mesaj> Mesajlar { get; set; } = new List<Mesaj>();
}