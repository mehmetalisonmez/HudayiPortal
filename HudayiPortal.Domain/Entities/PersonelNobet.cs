using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class PersonelNobet : Entity
{
	public Guid PersonelId { get; set; } // Kullanicilar tablosundaki personel
	public DateTime BaslangicTarihi { get; set; }
	public DateTime BitisTarihi { get; set; }
	public string NobetTuru { get; set; } // "Gece Nöbeti", "Haftasonu", "İzinli"

	public Kullanici Personel { get; set; }
}