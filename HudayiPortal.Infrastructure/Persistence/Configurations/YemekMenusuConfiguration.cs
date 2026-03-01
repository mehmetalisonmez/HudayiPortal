using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HudayiPortal.Infrastructure.Persistence.Configurations;

public class YemekMenusuConfiguration : IEntityTypeConfiguration<YemekMenusu>
{
	public void Configure(EntityTypeBuilder<YemekMenusu> builder)
	{
		builder.ToTable("YemekMenuleri");

		builder.HasKey(y => y.Id);

		builder.Property(y => y.Tarih)
			.HasColumnType("date")
			.IsRequired();

		builder.Property(y => y.OgunTuruId)
			.IsRequired();

		builder.Property(y => y.OlusturulmaTarihi)
			.HasDefaultValueSql("getdate()");

		builder.Property(y => y.SilindiMi)
			.HasDefaultValue(false);

		// Relationships — 6 FKs to YemekTanimlari
		builder.HasOne(y => y.Corba)
			.WithMany(t => t.CorbaMenuleri)
			.HasForeignKey(y => y.CorbaId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(y => y.AnaYemek)
			.WithMany(t => t.AnaYemekMenuleri)
			.HasForeignKey(y => y.AnaYemekId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(y => y.YardimciYemek)
			.WithMany(t => t.YardimciYemekMenuleri)
			.HasForeignKey(y => y.YardimciYemekId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(y => y.Ekstra)
			.WithMany(t => t.EkstraMenuleri)
			.HasForeignKey(y => y.EkstraId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(y => y.KahvaltiSicak1)
			.WithMany(t => t.KahvaltiSicak1Menuleri)
			.HasForeignKey(y => y.KahvaltiSicak1Id)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(y => y.KahvaltiSicak2)
			.WithMany(t => t.KahvaltiSicak2Menuleri)
			.HasForeignKey(y => y.KahvaltiSicak2Id)
			.OnDelete(DeleteBehavior.NoAction);
	}
}