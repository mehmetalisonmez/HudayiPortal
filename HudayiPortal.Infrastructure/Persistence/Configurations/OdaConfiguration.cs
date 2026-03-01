using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class OdaConfiguration : IEntityTypeConfiguration<Oda>
{
	public void Configure(EntityTypeBuilder<Oda> builder)
	{
		builder.ToTable("Odalar");

		builder.HasKey(o => o.Id);

		builder.Property(o => o.OdaNo)
			.IsRequired()
			.HasMaxLength(20);

		builder.Property(o => o.Kapasite)
			.IsRequired();

		builder.Property(o => o.Kat)
			.IsRequired();

		builder.Property(o => o.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(o => o.SilindiMi)
			.HasDefaultValue(false);
	}
}