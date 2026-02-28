using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class ChatGrupUye : Entity
{
	public Guid ChatGrupId { get; set; }
	public Guid KullaniciId { get; set; }

	public bool IsAdmin { get; set; } // Yönetici mi?
	public DateTime KatilmaTarihi { get; set; } // Ne zaman katıldı?

	// --- Navigation Properties ---
	public ChatGrup ChatGrup { get; set; }
	public Kullanici Kullanici { get; set; }
}