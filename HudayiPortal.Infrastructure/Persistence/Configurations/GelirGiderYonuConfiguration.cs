using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class GelirGiderYonuConfiguration : IEntityTypeConfiguration<GelirGiderYonu>
{
    public void Configure(EntityTypeBuilder<GelirGiderYonu> builder)
    {
        builder.ToTable("GelirGiderYonu");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.YonAdi)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(g => g.OlusturulmaTarihi)
            .HasDefaultValueSql("getdate()");

        builder.Property(g => g.SilindiMi)
            .HasDefaultValue(false);
    }
}