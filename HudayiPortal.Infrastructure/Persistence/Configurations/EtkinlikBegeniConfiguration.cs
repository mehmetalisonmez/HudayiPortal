using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class EtkinlikBegeniConfiguration : IEntityTypeConfiguration<EtkinlikBegeni>
{
	public void Configure(EntityTypeBuilder<EtkinlikBegeni> builder)
	{
		builder.ToTable("EtkinlikBegenileri");

		builder.HasKey(e => e.Id);

		builder.Property(e => e.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		// Unique constraint: bir kullanıcı aynı etkinliği yalnızca bir kez beğenebilir
		builder.HasIndex(e => new { e.EtkinlikId, e.KullaniciId })
			.IsUnique();

		// Relationships
		builder.HasOne(e => e.Etkinlik)
			.WithMany(et => et.Begeniler)
			.HasForeignKey(e => e.EtkinlikId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(e => e.Kullanici)
			.WithMany()
			.HasForeignKey(e => e.KullaniciId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
