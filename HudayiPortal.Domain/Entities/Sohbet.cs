namespace HudayiPortal.Domain.Entities;

public class Sohbet
{
	public int Id { get; set; }
	public int SohbetGrupId { get; set; }
	public DateTime Tarih { get; set; }
	public string? KonuBasligi { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public SohbetGrubu SohbetGrup { get; set; } = null!;
	public ICollection<SohbetYoklama> SohbetYoklamalari { get; set; } = new List<SohbetYoklama>();
}