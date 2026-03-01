using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class MaliIslemConfiguration : IEntityTypeConfiguration<MaliIslem>
{
	public void Configure(EntityTypeBuilder<MaliIslem> builder)
	{
		builder.ToTable("MaliIslemler");

		builder.HasKey(m => m.Id);

		builder.Property(m => m.Baslik)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(m => m.Aciklama)
			.HasMaxLength(500);

		builder.Property(m => m.Tutar)
			.HasColumnType("decimal(18,2)")
			.IsRequired();

		builder.Property(m => m.IslemTarihi)
			.IsRequired();

		builder.Property(m => m.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(m => m.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(m => m.Yon)
			.WithMany(g => g.MaliIslemler)
			.HasForeignKey(m => m.YonId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(m => m.IlgiliKullanici)
			.WithMany(k => k.MaliIslemler)
			.HasForeignKey(m => m.IlgiliKullaniciId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}