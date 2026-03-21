using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class IzinTuruConfiguration : IEntityTypeConfiguration<IzinTuru>
{
	public void Configure(EntityTypeBuilder<IzinTuru> builder)
	{
		builder.ToTable("IzinTurleri");

		builder.HasKey(i => i.Id);

		builder.Property(i => i.TurAdi)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(i => i.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(i => i.SilindiMi)
			.HasDefaultValue(false);
	}
}
