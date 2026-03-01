using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class ChatGrupUyesiConfiguration : IEntityTypeConfiguration<ChatGrupUyesi>
{
	public void Configure(EntityTypeBuilder<ChatGrupUyesi> builder)
	{
		builder.ToTable("ChatGrupUyeleri");

		builder.HasKey(c => c.Id);

		builder.Property(c => c.IsAdmin)
			.HasDefaultValue(false);

		builder.Property(c => c.KatilmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(c => c.SilindiMi)
			.HasDefaultValue(false);

		// Relationships
		builder.HasOne(c => c.ChatGrup)
			.WithMany(g => g.ChatGrupUyeleri)
			.HasForeignKey(c => c.ChatGrupId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(c => c.Kullanici)
			.WithMany(k => k.ChatGrupUyelikleri)
			.HasForeignKey(c => c.KullaniciId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}