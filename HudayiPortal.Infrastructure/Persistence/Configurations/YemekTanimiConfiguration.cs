using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class YemekTanimiConfiguration : IEntityTypeConfiguration<YemekTanimi>
{
	public void Configure(EntityTypeBuilder<YemekTanimi> builder)
	{
		builder.ToTable("YemekTanimlari");

		builder.HasKey(y => y.Id);

		builder.Property(y => y.YemekAdi)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(y => y.ResimUrl)
			.HasMaxLength(250);

		builder.Property(y => y.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(y => y.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(y => y.Kategori)
			.WithMany(k => k.YemekTanimlari)
			.HasForeignKey(y => y.KategoriId)
			.OnDelete(DeleteBehavior.NoAction);

		// Başlangıç verileri (Seed Data)
		var seed = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		builder.HasData(
			// Çorbalar (KategoriId = 101)
			new YemekTanimi { Id = 101, YemekAdi = "Mercimek Çorbası",    KategoriId = 101, Kalori = 150, OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 102, YemekAdi = "Ezogelin Çorbası",    KategoriId = 101, Kalori = 130, OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 103, YemekAdi = "Domates Çorbası",     KategoriId = 101, Kalori = 120, OlusturulmaTarihi = seed, SilindiMi = false },
			// Ana Yemekler (KategoriId = 102)
			new YemekTanimi { Id = 104, YemekAdi = "Kuru Fasulye",        KategoriId = 102, Kalori = 350, OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 105, YemekAdi = "Orman Kebabı",        KategoriId = 102, Kalori = 400, OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 106, YemekAdi = "Tavuk Sote",          KategoriId = 102, Kalori = 320, OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 107, YemekAdi = "Etli Nohut",          KategoriId = 102, Kalori = 380, OlusturulmaTarihi = seed, SilindiMi = false },
			// Yardımcı Yemekler (KategoriId = 103)
			new YemekTanimi { Id = 108, YemekAdi = "Pirinç Pilavı",       KategoriId = 103, Kalori = 250, OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 109, YemekAdi = "Bulgur Pilavı",       KategoriId = 103, Kalori = 230, OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 110, YemekAdi = "Makarna",             KategoriId = 103, Kalori = 280, OlusturulmaTarihi = seed, SilindiMi = false },
			// Ekstralar (KategoriId = 104)
			new YemekTanimi { Id = 111, YemekAdi = "Cacık",               KategoriId = 104, Kalori = 80,  OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 112, YemekAdi = "Mevsim Salata",       KategoriId = 104, Kalori = 60,  OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 113, YemekAdi = "Ayran",               KategoriId = 104, Kalori = 70,  OlusturulmaTarihi = seed, SilindiMi = false },
			// Kahvaltılık Sıcaklar (KategoriId = 105)
			new YemekTanimi { Id = 114, YemekAdi = "Menemen",             KategoriId = 105, Kalori = 220, OlusturulmaTarihi = seed, SilindiMi = false },
			new YemekTanimi { Id = 115, YemekAdi = "Sahanda Yumurta",     KategoriId = 105, Kalori = 200, OlusturulmaTarihi = seed, SilindiMi = false }
		);
	}
}