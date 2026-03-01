using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class KullaniciConfiguration : IEntityTypeConfiguration<Kullanici>
{
	public void Configure(EntityTypeBuilder<Kullanici> builder)
	{
		builder.ToTable("Kullanicilar");

		builder.HasKey(k => k.Id);

		builder.Property(k => k.Ad)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(k => k.Soyad)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(k => k.TcKimlikNo)
			.HasMaxLength(11);

		builder.Property(k => k.Telefon)
			.HasMaxLength(15);

		builder.Property(k => k.Email)
			.HasMaxLength(100);

		builder.Property(k => k.KanGrubu)
			.HasMaxLength(10);

		builder.Property(k => k.ProfilResmiUrl)
			.HasMaxLength(250);

		builder.Property(k => k.EmailDogrulandiMi)
			.HasDefaultValue(false);

		builder.Property(k => k.AktifMi)
			.HasDefaultValue(true);

		builder.Property(k => k.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(k => k.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(k => k.Rol)
			.WithMany(r => r.Kullanicilar)
			.HasForeignKey(k => k.RolId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(k => k.Oda)
			.WithMany(o => o.Kullanicilar)
			.HasForeignKey(k => k.OdaId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}