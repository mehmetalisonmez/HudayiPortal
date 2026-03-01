using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class GunlukYoklamaConfiguration : IEntityTypeConfiguration<GunlukYoklama>
{
	public void Configure(EntityTypeBuilder<GunlukYoklama> builder)
	{
		builder.ToTable("GunlukYoklamalar");

		builder.HasKey(g => g.Id);

		builder.Property(g => g.Tarih)
			.HasColumnType("date")
			.IsRequired();

		builder.Property(g => g.Durum)
			.IsRequired();

		builder.Property(g => g.Aciklama)
			.HasMaxLength(250);

		builder.Property(g => g.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(g => g.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(g => g.Kullanici)
			.WithMany(k => k.GunlukYoklamalari)
			.HasForeignKey(g => g.KullaniciId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(g => g.YoklamaTur)
			.WithMany(t => t.GunlukYoklamalar)
			.HasForeignKey(g => g.YoklamaTurId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(g => g.YoklamayiAlanPersonel)
			.WithMany(k => k.AlinanGunlukYoklamalar)
			.HasForeignKey(g => g.YoklamayiAlanPersonelId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}