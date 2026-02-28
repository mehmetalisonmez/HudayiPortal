using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class StandartKahvaltiUrun : Entity
{
	public string UrunAdi { get; set; } // Örn: "Siyah Zeytin", "Bal", "Tereyağı"
	public string? Birim { get; set; }    // Örn: "Adet", "Gram", "Porsiyon"
	public string? Kalori { get; set; }   // Opsiyonel bilgi
	public string? ResimUrl { get; set; } // Ürün görseli
	public bool AktifMi { get; set; }     // O hafta menüde var mı/yok mu?
}