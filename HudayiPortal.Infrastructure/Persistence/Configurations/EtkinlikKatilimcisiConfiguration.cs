using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class EtkinlikKatilimcisiConfiguration : IEntityTypeConfiguration<EtkinlikKatilimcisi>
{
	public void Configure(EntityTypeBuilder<EtkinlikKatilimcisi> builder)
	{
		builder.ToTable("EtkinlikKatilimcilari");

		builder.HasKey(e => e.Id);

		builder.Property(e => e.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(e => e.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(e => e.Etkinlik)
			.WithMany(et => et.EtkinlikKatilimcilari)
			.HasForeignKey(e => e.EtkinlikId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(e => e.Kullanici)
			.WithMany(k => k.EtkinlikKatilimlari)
			.HasForeignKey(e => e.KullaniciId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}