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

		// Başlangıç verileri (Seed Data)
		builder.HasData(
			new IzinTuru { Id = 11, TurAdi = "Evci İzni", OlusturulmaTarihi = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), SilindiMi = false },
			new IzinTuru { Id = 12, TurAdi = "Hastalık İzni", OlusturulmaTarihi = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), SilindiMi = false },
			new IzinTuru { Id = 13, TurAdi = "Sosyal Faaliyet İzni", OlusturulmaTarihi = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), SilindiMi = false },
			new IzinTuru { Id = 14, TurAdi = "Mazeret İzni", OlusturulmaTarihi = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), SilindiMi = false }
		);
	}
}
