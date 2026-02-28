using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class Sikayet : Entity
{
	public Guid GonderenKullaniciId { get; set; }
	public string Baslik { get; set; }
	public string Icerik { get; set; }
	public string? Cevap { get; set; }
	public string Durum { get; set; } // "Bekliyor", "İnceleniyor", "Çözüldü"
	public DateTime? CevaplanmaTarihi { get; set; }

	public Kullanici GonderenKullanici { get; set; }
}