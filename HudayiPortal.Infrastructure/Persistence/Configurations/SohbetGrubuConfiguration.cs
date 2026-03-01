using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class SohbetGrubuConfiguration : IEntityTypeConfiguration<SohbetGrubu>
{
	public void Configure(EntityTypeBuilder<SohbetGrubu> builder)
	{
		builder.ToTable("SohbetGruplari");

		builder.HasKey(s => s.Id);

		builder.Property(s => s.GrupAdi)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(s => s.SorumluHocaAdi)
			.HasMaxLength(100);

		builder.Property(s => s.Donem)
			.HasMaxLength(20);

		builder.Property(s => s.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(s => s.SilindiMi)
			.HasDefaultValue(false);
	}
}