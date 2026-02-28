namespace HudayiPortal.Domain.Abstraction;
public abstract class Entity
{
	public Guid Id { get; set; }
	public DateTime OlusturulmaTarihi { get; set; }
	public DateTime? GuncellenmeTarihi { get; set; }
	public bool SilindiMi { get; set; }

	public Entity()
	{
		Id = Guid.NewGuid(); // Otomatik Guid ataması
		OlusturulmaTarihi = DateTime.Now;
		SilindiMi = false;
	}
}
