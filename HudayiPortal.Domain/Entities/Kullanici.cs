using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class Kullanici : Entity
{
	public string Ad { get; set; } = default!;
	public string Soyad { get; set; } = default!;
	public string TcKimlikNo { get; set; } = default!;
	public string Telefon { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string SifreHash { get; set; } = default!;
	public DateTime DogumTarihi { get; set; }
	public string KanGrubu { get; set; } = default!;
	public string? ProfilResmiUrl { get; set; }

	public bool EmailDogrulandiMi { get; set; }
	public bool AktifMi { get; set; }

	// --- Foreign Keys ---
	public Guid RolId { get; set; }
	public Guid? OdaId { get; set; }

	// --- Navigation Properties (Tekil) ---
	public Rol Rol { get; set; }
	public Oda? Oda { get; set; }

	// --- Navigation Properties (Çoğul / Listeler) ---
	// 1. Şikayetler
	public ICollection<Sikayet> GonderilenSikayetler { get; set; }

	// 2. Mesajlaşma (Gönderdiği ve Aldığı mesajları ayırıyoruz)
	public ICollection<Mesaj> GonderilenMesajlar { get; set; }
	public ICollection<Mesaj> AlinanMesajlar { get; set; }

	// 3. Sohbet Grupları (Kendisinin kurduğu gruplar)
	public ICollection<ChatGrup> OlusturulanChatGruplari { get; set; }

	// 4. Grup Üyelikleri (Hangi gruplara üye olduğu)
	public ICollection<ChatGrupUye> ChatGrupUyelikleri { get; set; }
}
