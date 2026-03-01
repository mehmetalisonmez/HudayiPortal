namespace HudayiPortal.Domain.Entities;

public class SohbetGrubu
{
	public int Id { get; set; }
	public string GrupAdi { get; set; } = null!;
	public string? SorumluHocaAdi { get; set; }
	public string? Donem { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public ICollection<OgrenciSohbetGrubu> OgrenciSohbetGruplari { get; set; } = new List<OgrenciSohbetGrubu>();
	public ICollection<Sohbet> Sohbetler { get; set; } = new List<Sohbet>();
}