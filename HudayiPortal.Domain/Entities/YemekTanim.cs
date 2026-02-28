using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class YemekTanim : Entity
{
	public Guid KategoriId { get; set; }
	public string Ad { get; set; } // "Mercimek Çorbası", "İzmir Köfte"
	public string? Kalori { get; set; }
	public string? ResimUrl { get; set; }

	public YemekKategori Kategori { get; set; }
}
