using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class IzinConfiguration : IEntityTypeConfiguration<Izin>
{
	public void Configure(EntityTypeBuilder<Izin> builder)
	{
		builder.ToTable("Izinler");

		builder.HasKey(i => i.Id);

		builder.Property(i => i.GidecegiAdres)
			.IsRequired()
			.HasMaxLength(250);

		builder.Property(i => i.Aciklama)
			.IsRequired()
			.HasMaxLength(500);

		builder.Property(i => i.OnayDurumu)
			.HasDefaultValue(0);

		builder.Property(i => i.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(i => i.SilindiMi)
			.HasDefaultValue(false);

		builder.HasOne(i => i.Kullanici)
			.WithMany(k => k.IzinTalepleri)
			.HasForeignKey(i => i.KullaniciId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(i => i.IzinTuru)
			.WithMany(t => t.Izinler)
			.HasForeignKey(i => i.IzinTurId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(i => i.OnaylayanPersonel)
			.WithMany(k => k.OnaylananIzinTalepleri)
			.HasForeignKey(i => i.OnaylayanPersonelId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
