using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class SikayetConfiguration : IEntityTypeConfiguration<Sikayet>
{
	public void Configure(EntityTypeBuilder<Sikayet> builder)
	{
		builder.ToTable("Sikayetler");

		builder.HasKey(s => s.Id);

		builder.Property(s => s.Baslik)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(s => s.Icerik)
			.IsRequired();

		builder.Property(s => s.Durum)
			.HasDefaultValue(0);

		builder.Property(s => s.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(s => s.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(s => s.GonderenKullanici)
			.WithMany(k => k.Sikayetler)
			.HasForeignKey(s => s.GonderenKullaniciId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}