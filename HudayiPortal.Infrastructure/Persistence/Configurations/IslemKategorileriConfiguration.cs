using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class IslemKategorileriConfiguration : IEntityTypeConfiguration<IslemKategorileri>
{
	public void Configure(EntityTypeBuilder<IslemKategorileri> builder)
	{
		builder.ToTable("IslemKategorileri");

		builder.HasKey(k => k.Id);

		builder.Property(k => k.KategoriAdi)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(k => k.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(k => k.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(k => k.Yon)
			.WithMany(g => g.IslemKategorileri)
			.HasForeignKey(k => k.YonId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
