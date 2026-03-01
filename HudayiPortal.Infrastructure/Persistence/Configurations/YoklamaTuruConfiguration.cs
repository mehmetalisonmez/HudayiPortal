using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class YoklamaTuruConfiguration : IEntityTypeConfiguration<YoklamaTuru>
{
	public void Configure(EntityTypeBuilder<YoklamaTuru> builder)
	{
		builder.ToTable("YoklamaTurleri");

		builder.HasKey(y => y.Id);

		builder.Property(y => y.TurAdi)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(y => y.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(y => y.SilindiMi)
			.HasDefaultValue(false);
	}
}