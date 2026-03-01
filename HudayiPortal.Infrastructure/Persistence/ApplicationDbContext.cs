using HudayiPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Rol> Roller => Set<Rol>();
	public DbSet<Oda> Odalar => Set<Oda>();
	public DbSet<Kullanici> Kullanicilar => Set<Kullanici>();
	public DbSet<ChatGrubu> ChatGruplari => Set<ChatGrubu>();
	public DbSet<ChatGrupUyesi> ChatGrupUyeleri => Set<ChatGrupUyesi>();
	public DbSet<Duyuru> Duyurular => Set<Duyuru>();
	public DbSet<Etkinlik> Etkinlikler => Set<Etkinlik>();
	public DbSet<EtkinlikKatilimcisi> EtkinlikKatilimcilari => Set<EtkinlikKatilimcisi>();
	public DbSet<EtkinlikYorumu> EtkinlikYorumlari => Set<EtkinlikYorumu>();
	public DbSet<GelirGiderYonu> GelirGiderYonleri => Set<GelirGiderYonu>();
	public DbSet<GunlukYoklama> GunlukYoklamalar => Set<GunlukYoklama>();
	public DbSet<MaliIslem> MaliIslemler => Set<MaliIslem>();
	public DbSet<Mesaj> Mesajlar => Set<Mesaj>();
	public DbSet<OgrenciSohbetGrubu> OgrenciSohbetGruplari => Set<OgrenciSohbetGrubu>();
	public DbSet<PersonelNobeti> PersonelNobetleri => Set<PersonelNobeti>();
	public DbSet<Sikayet> Sikayetler => Set<Sikayet>();
	public DbSet<SohbetGrubu> SohbetGruplari => Set<SohbetGrubu>();
	public DbSet<Sohbet> Sohbetler => Set<Sohbet>();
	public DbSet<SohbetYoklama> SohbetYoklamalar => Set<SohbetYoklama>();
	public DbSet<YoklamaTuru> YoklamaTurleri => Set<YoklamaTuru>();
	public DbSet<YemekKategorisi> YemekKategorileri => Set<YemekKategorisi>();
	public DbSet<YemekTanimi> YemekTanimlari => Set<YemekTanimi>();
	public DbSet<StandartKahvaltiUrunu> StandartKahvaltiUrunleri => Set<StandartKahvaltiUrunu>();
	public DbSet<YemekMenusu> YemekMenuleri => Set<YemekMenusu>();
	public DbSet<YemekYorumu> YemekYorumlari => Set<YemekYorumu>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}