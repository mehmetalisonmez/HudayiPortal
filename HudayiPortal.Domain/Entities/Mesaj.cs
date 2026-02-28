using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class Mesaj : Entity
{
	public Guid GondericiId { get; set; }

	// Eğer AliciId doluysa -> Bireysel Mesaj (DM)
	// Eğer ChatGrupId doluysa -> Grup Mesajı
	public Guid? AliciId { get; set; }
	public Guid? ChatGrupId { get; set; }

	public string Icerik { get; set; } = default!;
	public bool OkunduMu { get; set; }

	// --- Navigation Properties ---
	public Kullanici Gonderici { get; set; }
	public Kullanici? Alici { get; set; }
	public ChatGrup? ChatGrup { get; set; }
}