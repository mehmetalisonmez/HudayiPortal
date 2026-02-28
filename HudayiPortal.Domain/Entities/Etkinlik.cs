using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class Etkinlik : Entity
{
	public string Baslik { get; set; }
	public string Aciklama { get; set; }
	public DateTime BaslangicTarihi { get; set; }
	public DateTime BitisTarihi { get; set; }
	public DateTime SonKayitTarihi { get; set; }
	public decimal Ucret { get; set; }
	public bool ZorunluMu { get; set; }
	public int Kontenjan { get; set; }
	public string? ResimUrl { get; set; }

	public ICollection<EtkinlikKatilimci> Katilimcilar { get; set; }
}
