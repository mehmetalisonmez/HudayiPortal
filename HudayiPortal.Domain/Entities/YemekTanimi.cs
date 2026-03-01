namespace HudayiPortal.Domain.Entities;

public class YemekTanimi
{
	public int Id { get; set; }
	public string YemekAdi { get; set; } = null!;
	public int KategoriId { get; set; }
	public int? Kalori { get; set; }
	public string? ResimUrl { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public YemekKategorisi Kategori { get; set; } = null!;
	public ICollection<StandartKahvaltiUrunu> StandartKahvaltiUrunleri { get; set; } = new List<StandartKahvaltiUrunu>();
	public ICollection<YemekMenusu> CorbaMenuleri { get; set; } = new List<YemekMenusu>();
	public ICollection<YemekMenusu> AnaYemekMenuleri { get; set; } = new List<YemekMenusu>();
	public ICollection<YemekMenusu> YardimciYemekMenuleri { get; set; } = new List<YemekMenusu>();
	public ICollection<YemekMenusu> EkstraMenuleri { get; set; } = new List<YemekMenusu>();
	public ICollection<YemekMenusu> KahvaltiSicak1Menuleri { get; set; } = new List<YemekMenusu>();
	public ICollection<YemekMenusu> KahvaltiSicak2Menuleri { get; set; } = new List<YemekMenusu>();
}