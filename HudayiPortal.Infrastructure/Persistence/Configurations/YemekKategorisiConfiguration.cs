using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class YemekKategorisiConfiguration : IEntityTypeConfiguration<YemekKategorisi>
{
	public void Configure(EntityTypeBuilder<YemekKategorisi> builder)
	{
		builder.ToTable("YemekKategorileri");

		builder.HasKey(y => y.Id);

		builder.Property(y => y.KategoriAdi)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(y => y.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(y => y.SilindiMi)
			.HasDefaultValue(false);
	}
}