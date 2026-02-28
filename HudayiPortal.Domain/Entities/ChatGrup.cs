using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class ChatGrup : Entity
{
	public string GrupAdi { get; set; } = default!;
	public string? ProfilResmiUrl { get; set; }

	// Grubu kim kurdu?
	public Guid OlusturanKullaniciId { get; set; }

	// --- Navigation Properties ---
	public Kullanici OlusturanKullanici { get; set; }
	public ICollection<ChatGrupUye> Uyeler { get; set; }
	public ICollection<Mesaj> Mesajlar { get; set; }
}
