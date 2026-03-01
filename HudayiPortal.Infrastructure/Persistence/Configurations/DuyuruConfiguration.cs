using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class DuyuruConfiguration : IEntityTypeConfiguration<Duyuru>
{
	public void Configure(EntityTypeBuilder<Duyuru> builder)
	{
		builder.ToTable("Duyurular");

		builder.HasKey(d => d.Id);

		builder.Property(d => d.Baslik)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(d => d.Icerik)
			.IsRequired();

		builder.Property(d => d.YayinTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(d => d.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(d => d.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(d => d.HedefRol)
			.WithMany(r => r.Duyurular)
			.HasForeignKey(d => d.HedefRolId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}