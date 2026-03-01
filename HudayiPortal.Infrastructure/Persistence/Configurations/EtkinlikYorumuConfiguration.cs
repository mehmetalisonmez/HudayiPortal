using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class EtkinlikYorumuConfiguration : IEntityTypeConfiguration<EtkinlikYorumu>
{
	public void Configure(EntityTypeBuilder<EtkinlikYorumu> builder)
	{
		builder.ToTable("EtkinlikYorumlari");

		builder.HasKey(e => e.Id);

		builder.Property(e => e.YorumMetni)
			.IsRequired()
			.HasMaxLength(500);

		builder.Property(e => e.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(e => e.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(e => e.Etkinlik)
			.WithMany(et => et.EtkinlikYorumlari)
			.HasForeignKey(e => e.EtkinlikId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(e => e.Kullanici)
			.WithMany(k => k.EtkinlikYorumlari)
			.HasForeignKey(e => e.KullaniciId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}