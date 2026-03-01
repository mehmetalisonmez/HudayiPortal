using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class MesajConfiguration : IEntityTypeConfiguration<Mesaj>
{
	public void Configure(EntityTypeBuilder<Mesaj> builder)
	{
		builder.ToTable("Mesajlar");

		builder.HasKey(m => m.Id);

		builder.Property(m => m.MesajIcerigi)
			.IsRequired();

		builder.Property(m => m.OkunduMu)
			.HasDefaultValue(false);

		builder.Property(m => m.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(m => m.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(m => m.Gonderen)
			.WithMany(k => k.GonderilenMesajlar)
			.HasForeignKey(m => m.GonderenId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(m => m.Alici)
			.WithMany(k => k.AlinanMesajlar)
			.HasForeignKey(m => m.AliciId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(m => m.ChatGrup)
			.WithMany(g => g.Mesajlar)
			.HasForeignKey(m => m.ChatGrupId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}