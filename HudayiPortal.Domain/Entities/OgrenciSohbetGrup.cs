using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class OgrenciSohbetGrup : Entity
{
	public Guid SohbetGrupId { get; set; }
	public Guid OgrenciId { get; set; }

	public SohbetGrup SohbetGrup { get; set; }
	public Kullanici Ogrenci { get; set; }
}