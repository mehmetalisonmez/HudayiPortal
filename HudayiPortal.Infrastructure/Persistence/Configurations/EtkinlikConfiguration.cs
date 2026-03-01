using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class EtkinlikConfiguration : IEntityTypeConfiguration<Etkinlik>
{
	public void Configure(EntityTypeBuilder<Etkinlik> builder)
	{
		builder.ToTable("Etkinlikler");

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Baslik)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(e => e.BaslangicTarihi)
			.IsRequired();

		builder.Property(e => e.Ucret)
			.HasColumnType("decimal(18,2)")
			.HasDefaultValue(0m);

		builder.Property(e => e.ZorunluMu)
			.HasDefaultValue(false);

		builder.Property(e => e.ResimUrl)
			.HasMaxLength(250);

		builder.Property(e => e.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(e => e.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(e => e.OlusturanPersonel)
			.WithMany(k => k.OlusturulanEtkinlikler)
			.HasForeignKey(e => e.OlusturanPersonelId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}