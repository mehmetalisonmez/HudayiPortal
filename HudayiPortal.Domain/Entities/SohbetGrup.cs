using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class SohbetGrup : Entity
{
	public string GrupAdi { get; set; }
	public Guid SorumluHocaId { get; set; } // Belletmen veya Hoca
	public string Donem { get; set; }       // Örn: "2025-2026 Güz"

	public Kullanici SorumluHoca { get; set; }
	public ICollection<OgrenciSohbetGrup> Ogrenciler { get; set; }
	public ICollection<Sohbet> Sohbetler { get; set; }
}