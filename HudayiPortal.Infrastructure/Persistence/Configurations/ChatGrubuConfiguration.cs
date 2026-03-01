using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class ChatGrubuConfiguration : IEntityTypeConfiguration<ChatGrubu>
{
	public void Configure(EntityTypeBuilder<ChatGrubu> builder)
	{
		builder.ToTable("ChatGruplari");

		builder.HasKey(c => c.Id);

		builder.Property(c => c.GrupAdi)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(c => c.GrupResmiUrl)
			.HasMaxLength(250);

		builder.Property(c => c.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(c => c.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(c => c.OlusturanKullanici)
			.WithMany(k => k.OlusturulanChatGruplari)
			.HasForeignKey(c => c.OlusturanKullaniciId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}