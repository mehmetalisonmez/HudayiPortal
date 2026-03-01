using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class YemekTanimiConfiguration : IEntityTypeConfiguration<YemekTanimi>
{
	public void Configure(EntityTypeBuilder<YemekTanimi> builder)
	{
		builder.ToTable("YemekTanimlari");

		builder.HasKey(y => y.Id);

		builder.Property(y => y.YemekAdi)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(y => y.ResimUrl)
			.HasMaxLength(250);

		builder.Property(y => y.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(y => y.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(y => y.Kategori)
			.WithMany(k => k.YemekTanimlari)
			.HasForeignKey(y => y.KategoriId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}