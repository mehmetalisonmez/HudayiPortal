using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class MaliIslem : Entity
{
	public Guid YonId { get; set; } // GelirGiderYon FK
	public Guid? IlgiliKullaniciId { get; set; } // Maaş ödemesi veya Aidat alınan kişi

	public string Baslik { get; set; }
	public decimal Tutar { get; set; }
	public DateTime Tarih { get; set; }
	public string? Aciklama { get; set; }

	public GelirGiderYon Yon { get; set; }
	public Kullanici? IlgiliKullanici { get; set; }
}
