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

		// Başlangıç verileri (Seed Data)
		builder.HasData(
			new YemekKategorisi { Id = 101, KategoriAdi = "Çorba", OlusturulmaTarihi = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), SilindiMi = false },
			new YemekKategorisi { Id = 102, KategoriAdi = "Ana Yemek", OlusturulmaTarihi = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), SilindiMi = false },
			new YemekKategorisi { Id = 103, KategoriAdi = "Yardımcı Yemek", OlusturulmaTarihi = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), SilindiMi = false },
			new YemekKategorisi { Id = 104, KategoriAdi = "Ekstra", OlusturulmaTarihi = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), SilindiMi = false },
			new YemekKategorisi { Id = 105, KategoriAdi = "Kahvaltılık Sıcak", OlusturulmaTarihi = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), SilindiMi = false }
		);
	}
}