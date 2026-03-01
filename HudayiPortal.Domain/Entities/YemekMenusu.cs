namespace HudayiPortal.Domain.Entities;

public class YemekMenusu
{
	public int Id { get; set; }
	public DateOnly Tarih { get; set; }
	public int OgunTuruId { get; set; }
	public int? CorbaId { get; set; }
	public int? AnaYemekId { get; set; }
	public int? YardimciYemekId { get; set; }
	public int? EkstraId { get; set; }
	public int? KahvaltiSicak1Id { get; set; }
	public int? KahvaltiSicak2Id { get; set; }
	public DateTime? OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool? SilindiMi { get; set; }

	// Navigation properties
	public YemekTanimi? Corba { get; set; }
	public YemekTanimi? AnaYemek { get; set; }
	public YemekTanimi? YardimciYemek { get; set; }
	public YemekTanimi? Ekstra { get; set; }
	public YemekTanimi? KahvaltiSicak1 { get; set; }
	public YemekTanimi? KahvaltiSicak2 { get; set; }
	public ICollection<YemekYorumu> YemekYorumlari { get; set; } = new List<YemekYorumu>();
}