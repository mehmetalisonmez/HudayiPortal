using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class StandartKahvaltiUrunuConfiguration : IEntityTypeConfiguration<StandartKahvaltiUrunu>
{
	public void Configure(EntityTypeBuilder<StandartKahvaltiUrunu> builder)
	{
		builder.ToTable("StandartKahvaltiUrunleri");

		builder.HasKey(s => s.Id);

		builder.Property(s => s.AktifMi)
			.HasDefaultValue(true);

		builder.Property(s => s.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(s => s.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(s => s.YemekTanim)
			.WithMany(y => y.StandartKahvaltiUrunleri)
			.HasForeignKey(s => s.YemekTanimId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}