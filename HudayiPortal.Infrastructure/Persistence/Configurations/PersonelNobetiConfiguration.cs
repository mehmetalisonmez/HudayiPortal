using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class PersonelNobetiConfiguration : IEntityTypeConfiguration<PersonelNobeti>
{
	public void Configure(EntityTypeBuilder<PersonelNobeti> builder)
	{
		builder.ToTable("PersonelNobetleri");

		builder.HasKey(p => p.Id);

		builder.Property(p => p.Tarih)
			.HasColumnType("date")
			.IsRequired();

		builder.Property(p => p.NobetTuru)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(p => p.Aciklama)
			.HasMaxLength(200);

		builder.Property(p => p.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(p => p.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(p => p.Personel)
			.WithMany(k => k.PersonelNobetleri)
			.HasForeignKey(p => p.PersonelId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}