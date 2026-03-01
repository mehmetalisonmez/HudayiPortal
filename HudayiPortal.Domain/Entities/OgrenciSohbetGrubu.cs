namespace HudayiPortal.Domain.Entities;

public class OgrenciSohbetGrubu
{
	public int Id { get; set; }
	public int KullaniciId { get; set; }
	public int SohbetGrupId { get; set; }
	public DateTime? AtanmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public Kullanici Kullanici { get; set; } = null!;
	public SohbetGrubu SohbetGrup { get; set; } = null!;
}