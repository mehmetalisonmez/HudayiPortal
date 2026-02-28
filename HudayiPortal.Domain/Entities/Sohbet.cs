using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class Sohbet : Entity
{
	public Guid SohbetGrupId { get; set; }
	public DateTime Tarih { get; set; }
	public string KonuBasligi { get; set; }

	public SohbetGrup SohbetGrup { get; set; }
	public ICollection<SohbetYoklama> Yoklamalar { get; set; }
}