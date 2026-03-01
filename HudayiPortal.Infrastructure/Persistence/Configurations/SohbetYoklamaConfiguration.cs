using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class SohbetYoklamaConfiguration : IEntityTypeConfiguration<SohbetYoklama>
{
	public void Configure(EntityTypeBuilder<SohbetYoklama> builder)
	{
		builder.ToTable("SohbetYoklamalar");

		builder.HasKey(s => s.Id);

		builder.Property(s => s.KatilimDurumu)
			.IsRequired();

		builder.Property(s => s.MazeretAciklamasi)
			.HasMaxLength(250);

		builder.Property(s => s.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(s => s.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(s => s.Sohbet)
			.WithMany(sh => sh.SohbetYoklamalari)
			.HasForeignKey(s => s.SohbetId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(s => s.Kullanici)
			.WithMany(k => k.SohbetYoklamalari)
			.HasForeignKey(s => s.KullaniciId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(s => s.YoklamayiAlanPersonel)
			.WithMany(k => k.AlinanSohbetYoklamalari)
			.HasForeignKey(s => s.YoklamayiAlanPersonelId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}