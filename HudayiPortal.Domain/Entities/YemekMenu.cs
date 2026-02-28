using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class YemekMenu : Entity
{
	public DateTime Tarih { get; set; }
	public string Ogun { get; set; } // "Kahvaltı", "Akşam"

	// Menü içeriği (YemekTanim tablosuna referanslar)
	// 3+1 kuralına uygun tasarım
	public Guid? CorbaId { get; set; }
	public Guid? AnaYemekId { get; set; }
	public Guid? YardimciYemekId { get; set; } // Pilav/Makarna
	public Guid? EkstraId { get; set; }        // Tatlı/Salata

	public YemekTanim? Corba { get; set; }
	public YemekTanim? AnaYemek { get; set; }
	public YemekTanim? YardimciYemek { get; set; }
	public YemekTanim? Ekstra { get; set; }

	public ICollection<YemekYorum> Yorumlar { get; set; }
}
