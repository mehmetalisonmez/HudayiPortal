using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class YemekYorumuConfiguration : IEntityTypeConfiguration<YemekYorumu>
{
	public void Configure(EntityTypeBuilder<YemekYorumu> builder)
	{
		builder.ToTable("YemekYorumlari");

		builder.HasKey(y => y.Id);

		builder.Property(y => y.YorumMetni)
			.IsRequired()
			.HasMaxLength(500);

		builder.Property(y => y.Puan)
			.HasDefaultValue(5);

		builder.Property(y => y.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(y => y.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(y => y.YemekMenu)
			.WithMany(m => m.YemekYorumlari)
			.HasForeignKey(y => y.YemekMenuId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(y => y.Kullanici)
			.WithMany(k => k.YemekYorumlari)
			.HasForeignKey(y => y.KullaniciId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}