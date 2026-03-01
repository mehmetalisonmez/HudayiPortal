using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
	public void Configure(EntityTypeBuilder<Rol> builder)
	{
		builder.ToTable("Roller");

		builder.HasKey(r => r.Id);

		builder.Property(r => r.RolAdi)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(r => r.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(r => r.SilindiMi)
			.HasDefaultValue(false);
	}
}