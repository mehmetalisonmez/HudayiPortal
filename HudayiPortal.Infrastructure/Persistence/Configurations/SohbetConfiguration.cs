using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class SohbetConfiguration : IEntityTypeConfiguration<Sohbet>
{
	public void Configure(EntityTypeBuilder<Sohbet> builder)
	{
		builder.ToTable("Sohbetler");

		builder.HasKey(s => s.Id);

		builder.Property(s => s.Tarih)
			.IsRequired();

		builder.Property(s => s.KonuBasligi)
			.HasMaxLength(150);

		builder.Property(s => s.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(s => s.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(s => s.SohbetGrup)
			.WithMany(g => g.Sohbetler)
			.HasForeignKey(s => s.SohbetGrupId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}