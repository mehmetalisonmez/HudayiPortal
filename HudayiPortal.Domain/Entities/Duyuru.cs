using HudayiPortal.Domain.Abstraction;

namespace HudayiPortal.Domain.Entities;
public sealed class Duyuru : Entity
{
	public string Baslik { get; set; }
	public string Icerik { get; set; }
	public Guid? HedefRolId { get; set; } // Null ise herkese açık
	public DateTime YayinTarihi { get; set; }

	public Rol? HedefRol { get; set; }
}