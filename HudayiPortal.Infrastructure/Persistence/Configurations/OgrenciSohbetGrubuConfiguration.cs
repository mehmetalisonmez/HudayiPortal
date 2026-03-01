using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class OgrenciSohbetGrubuConfiguration : IEntityTypeConfiguration<OgrenciSohbetGrubu>
{
	public void Configure(EntityTypeBuilder<OgrenciSohbetGrubu> builder)
	{
		builder.ToTable("OgrenciSohbetGruplari");

		builder.HasKey(o => o.Id);

		builder.Property(o => o.AtanmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(o => o.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(o => o.Kullanici)
			.WithMany(k => k.OgrenciSohbetGruplari)
			.HasForeignKey(o => o.KullaniciId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(o => o.SohbetGrup)
			.WithMany(s => s.OgrenciSohbetGruplari)
			.HasForeignKey(o => o.SohbetGrupId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}