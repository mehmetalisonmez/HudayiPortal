namespace HudayiPortal.Domain.Entities;

public class Kullanici
{
	public int Id { get; set; }
	public int RolId { get; set; }
	public int? OdaId { get; set; }
	public string Ad { get; set; } = null!;
	public string Soyad { get; set; } = null!;
	public string? TcKimlikNo { get; set; }
	public string? Telefon { get; set; }
	public string? Email { get; set; }
	public string? SifreHash { get; set; }
	public DateTime? DogumTarihi { get; set; }
	public string? KanGrubu { get; set; }
	public string? ProfilResmiUrl { get; set; }
	public bool? EmailDogrulandiMi { get; set; }
	public bool? AktifMi { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Rol Rol { get; set; } = null!;
	public Oda? Oda { get; set; }

	public ICollection<ChatGrubu> OlusturulanChatGruplari { get; set; } = new List<ChatGrubu>();
	public ICollection<ChatGrupUyesi> ChatGrupUyelikleri { get; set; } = new List<ChatGrupUyesi>();
	public ICollection<EtkinlikKatilimcisi> EtkinlikKatilimlari { get; set; } = new List<EtkinlikKatilimcisi>();
	public ICollection<Etkinlik> OlusturulanEtkinlikler { get; set; } = new List<Etkinlik>();
	public ICollection<EtkinlikYorumu> EtkinlikYorumlari { get; set; } = new List<EtkinlikYorumu>();
	public ICollection<GunlukYoklama> GunlukYoklamalari { get; set; } = new List<GunlukYoklama>();
	public ICollection<GunlukYoklama> AlinanGunlukYoklamalar { get; set; } = new List<GunlukYoklama>();
	public ICollection<MaliIslem> MaliIslemler { get; set; } = new List<MaliIslem>();
	public ICollection<Mesaj> GonderilenMesajlar { get; set; } = new List<Mesaj>();
	public ICollection<Mesaj> AlinanMesajlar { get; set; } = new List<Mesaj>();
	public ICollection<OgrenciSohbetGrubu> OgrenciSohbetGruplari { get; set; } = new List<OgrenciSohbetGrubu>();
	public ICollection<PersonelNobeti> PersonelNobetleri { get; set; } = new List<PersonelNobeti>();
	public ICollection<Sikayet> Sikayetler { get; set; } = new List<Sikayet>();
	public ICollection<SohbetYoklama> SohbetYoklamalari { get; set; } = new List<SohbetYoklama>();
	public ICollection<SohbetYoklama> AlinanSohbetYoklamalari { get; set; } = new List<SohbetYoklama>();
	public ICollection<YemekYorumu> YemekYorumlari { get; set; } = new List<YemekYorumu>();
}
