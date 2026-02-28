using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class GunlukYoklama : Entity
{
	public Guid YoklamaTurId { get; set; }
	public Guid OgrenciId { get; set; }
	public Guid YoklamayiAlanPersonelId { get; set; }

	public DateTime Tarih { get; set; }
	public bool VarMi { get; set; }
	public string? Aciklama { get; set; }

	public YoklamaTur YoklamaTur { get; set; }
	public Kullanici Ogrenci { get; set; }
	public Kullanici YoklamayiAlanPersonel { get; set; }
}