using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HudayiPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GelirGiderYonu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YonAdi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GelirGiderYonu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Odalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdaNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Kapasite = table.Column<int>(type: "int", nullable: false),
                    Kat = table.Column<int>(type: "int", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SohbetGruplari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SorumluHocaAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Donem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SohbetGruplari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YemekKategorileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekKategorileri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YoklamaTurleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoklamaTurleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Duyurular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HedefRolId = table.Column<int>(type: "int", nullable: true),
                    YayinTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyurular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Duyurular_Roller_HedefRolId",
                        column: x => x.HedefRolId,
                        principalTable: "Roller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    OdaId = table.Column<int>(type: "int", nullable: true),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SifreHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KanGrubu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ProfilResmiUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EmailDogrulandiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kullanicilar_Odalar_OdaId",
                        column: x => x.OdaId,
                        principalTable: "Odalar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kullanicilar_Roller_RolId",
                        column: x => x.RolId,
                        principalTable: "Roller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sohbetler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SohbetGrupId = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KonuBasligi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sohbetler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sohbetler_SohbetGruplari_SohbetGrupId",
                        column: x => x.SohbetGrupId,
                        principalTable: "SohbetGruplari",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YemekTanimlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YemekAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    Kalori = table.Column<int>(type: "int", nullable: true),
                    ResimUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekTanimlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YemekTanimlari_YemekKategorileri_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "YemekKategorileri",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatGruplari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GrupResmiUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGruplari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatGruplari_Kullanicilar_OlusturanKullaniciId",
                        column: x => x.OlusturanKullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Etkinlikler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SonKayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ucret = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m),
                    ZorunluMu = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    ResimUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OlusturanPersonelId = table.Column<int>(type: "int", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinlikler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etkinlikler_Kullanicilar_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GunlukYoklamalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    YoklamaTurId = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateOnly>(type: "date", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    YoklamayiAlanPersonelId = table.Column<int>(type: "int", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GunlukYoklamalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GunlukYoklamalar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GunlukYoklamalar_Kullanicilar_YoklamayiAlanPersonelId",
                        column: x => x.YoklamayiAlanPersonelId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GunlukYoklamalar_YoklamaTurleri_YoklamaTurId",
                        column: x => x.YoklamaTurId,
                        principalTable: "YoklamaTurleri",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaliIslemler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YonId = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IslemTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IlgiliKullaniciId = table.Column<int>(type: "int", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaliIslemler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaliIslemler_GelirGiderYonu_YonId",
                        column: x => x.YonId,
                        principalTable: "GelirGiderYonu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaliIslemler_Kullanicilar_IlgiliKullaniciId",
                        column: x => x.IlgiliKullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OgrenciSohbetGruplari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    SohbetGrupId = table.Column<int>(type: "int", nullable: false),
                    AtanmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciSohbetGruplari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrenciSohbetGruplari_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OgrenciSohbetGruplari_SohbetGruplari_SohbetGrupId",
                        column: x => x.SohbetGrupId,
                        principalTable: "SohbetGruplari",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonelNobetleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateOnly>(type: "date", nullable: false),
                    NobetTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelNobetleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelNobetleri_Kullanicilar_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sikayetler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderenKullaniciId = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cevap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    CevaplanmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sikayetler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sikayetler_Kullanicilar_GonderenKullaniciId",
                        column: x => x.GonderenKullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SohbetYoklamalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SohbetId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    KatilimDurumu = table.Column<bool>(type: "bit", nullable: false),
                    MazeretAciklamasi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    YoklamayiAlanPersonelId = table.Column<int>(type: "int", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SohbetYoklamalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SohbetYoklamalar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SohbetYoklamalar_Kullanicilar_YoklamayiAlanPersonelId",
                        column: x => x.YoklamayiAlanPersonelId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SohbetYoklamalar_Sohbetler_SohbetId",
                        column: x => x.SohbetId,
                        principalTable: "Sohbetler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StandartKahvaltiUrunleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YemekTanimId = table.Column<int>(type: "int", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandartKahvaltiUrunleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandartKahvaltiUrunleri_YemekTanimlari_YemekTanimId",
                        column: x => x.YemekTanimId,
                        principalTable: "YemekTanimlari",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YemekMenuleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarih = table.Column<DateOnly>(type: "date", nullable: false),
                    OgunTuruId = table.Column<int>(type: "int", nullable: false),
                    CorbaId = table.Column<int>(type: "int", nullable: true),
                    AnaYemekId = table.Column<int>(type: "int", nullable: true),
                    YardimciYemekId = table.Column<int>(type: "int", nullable: true),
                    EkstraId = table.Column<int>(type: "int", nullable: true),
                    KahvaltiSicak1Id = table.Column<int>(type: "int", nullable: true),
                    KahvaltiSicak2Id = table.Column<int>(type: "int", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekMenuleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YemekMenuleri_YemekTanimlari_AnaYemekId",
                        column: x => x.AnaYemekId,
                        principalTable: "YemekTanimlari",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YemekMenuleri_YemekTanimlari_CorbaId",
                        column: x => x.CorbaId,
                        principalTable: "YemekTanimlari",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YemekMenuleri_YemekTanimlari_EkstraId",
                        column: x => x.EkstraId,
                        principalTable: "YemekTanimlari",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YemekMenuleri_YemekTanimlari_KahvaltiSicak1Id",
                        column: x => x.KahvaltiSicak1Id,
                        principalTable: "YemekTanimlari",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YemekMenuleri_YemekTanimlari_KahvaltiSicak2Id",
                        column: x => x.KahvaltiSicak2Id,
                        principalTable: "YemekTanimlari",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YemekMenuleri_YemekTanimlari_YardimciYemekId",
                        column: x => x.YardimciYemekId,
                        principalTable: "YemekTanimlari",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatGrupUyeleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatGrupId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    KatilmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGrupUyeleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatGrupUyeleri_ChatGruplari_ChatGrupId",
                        column: x => x.ChatGrupId,
                        principalTable: "ChatGruplari",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatGrupUyeleri_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mesajlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderenId = table.Column<int>(type: "int", nullable: false),
                    AliciId = table.Column<int>(type: "int", nullable: true),
                    ChatGrupId = table.Column<int>(type: "int", nullable: true),
                    MesajIcerigi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OkunduMu = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesajlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mesajlar_ChatGruplari_ChatGrupId",
                        column: x => x.ChatGrupId,
                        principalTable: "ChatGruplari",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mesajlar_Kullanicilar_AliciId",
                        column: x => x.AliciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mesajlar_Kullanicilar_GonderenId",
                        column: x => x.GonderenId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EtkinlikKatilimcilari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtkinlikId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    KatilimDurumu = table.Column<bool>(type: "bit", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtkinlikKatilimcilari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtkinlikKatilimcilari_Etkinlikler_EtkinlikId",
                        column: x => x.EtkinlikId,
                        principalTable: "Etkinlikler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EtkinlikKatilimcilari_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EtkinlikYorumlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtkinlikId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    YorumMetni = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtkinlikYorumlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtkinlikYorumlari_Etkinlikler_EtkinlikId",
                        column: x => x.EtkinlikId,
                        principalTable: "Etkinlikler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EtkinlikYorumlari_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YemekYorumlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YemekMenuId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    YorumMetni = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Puan = table.Column<int>(type: "int", nullable: true, defaultValue: 5),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekYorumlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YemekYorumlari_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YemekYorumlari_YemekMenuleri_YemekMenuId",
                        column: x => x.YemekMenuId,
                        principalTable: "YemekMenuleri",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatGruplari_OlusturanKullaniciId",
                table: "ChatGruplari",
                column: "OlusturanKullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGrupUyeleri_ChatGrupId",
                table: "ChatGrupUyeleri",
                column: "ChatGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGrupUyeleri_KullaniciId",
                table: "ChatGrupUyeleri",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Duyurular_HedefRolId",
                table: "Duyurular",
                column: "HedefRolId");

            migrationBuilder.CreateIndex(
                name: "IX_EtkinlikKatilimcilari_EtkinlikId",
                table: "EtkinlikKatilimcilari",
                column: "EtkinlikId");

            migrationBuilder.CreateIndex(
                name: "IX_EtkinlikKatilimcilari_KullaniciId",
                table: "EtkinlikKatilimcilari",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_OlusturanPersonelId",
                table: "Etkinlikler",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_EtkinlikYorumlari_EtkinlikId",
                table: "EtkinlikYorumlari",
                column: "EtkinlikId");

            migrationBuilder.CreateIndex(
                name: "IX_EtkinlikYorumlari_KullaniciId",
                table: "EtkinlikYorumlari",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_GunlukYoklamalar_KullaniciId",
                table: "GunlukYoklamalar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_GunlukYoklamalar_YoklamaTurId",
                table: "GunlukYoklamalar",
                column: "YoklamaTurId");

            migrationBuilder.CreateIndex(
                name: "IX_GunlukYoklamalar_YoklamayiAlanPersonelId",
                table: "GunlukYoklamalar",
                column: "YoklamayiAlanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_OdaId",
                table: "Kullanicilar",
                column: "OdaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_MaliIslemler_IlgiliKullaniciId",
                table: "MaliIslemler",
                column: "IlgiliKullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_MaliIslemler_YonId",
                table: "MaliIslemler",
                column: "YonId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_AliciId",
                table: "Mesajlar",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_ChatGrupId",
                table: "Mesajlar",
                column: "ChatGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_GonderenId",
                table: "Mesajlar",
                column: "GonderenId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciSohbetGruplari_KullaniciId",
                table: "OgrenciSohbetGruplari",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciSohbetGruplari_SohbetGrupId",
                table: "OgrenciSohbetGruplari",
                column: "SohbetGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelNobetleri_PersonelId",
                table: "PersonelNobetleri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sikayetler_GonderenKullaniciId",
                table: "Sikayetler",
                column: "GonderenKullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Sohbetler_SohbetGrupId",
                table: "Sohbetler",
                column: "SohbetGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_SohbetYoklamalar_KullaniciId",
                table: "SohbetYoklamalar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_SohbetYoklamalar_SohbetId",
                table: "SohbetYoklamalar",
                column: "SohbetId");

            migrationBuilder.CreateIndex(
                name: "IX_SohbetYoklamalar_YoklamayiAlanPersonelId",
                table: "SohbetYoklamalar",
                column: "YoklamayiAlanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_StandartKahvaltiUrunleri_YemekTanimId",
                table: "StandartKahvaltiUrunleri",
                column: "YemekTanimId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekMenuleri_AnaYemekId",
                table: "YemekMenuleri",
                column: "AnaYemekId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekMenuleri_CorbaId",
                table: "YemekMenuleri",
                column: "CorbaId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekMenuleri_EkstraId",
                table: "YemekMenuleri",
                column: "EkstraId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekMenuleri_KahvaltiSicak1Id",
                table: "YemekMenuleri",
                column: "KahvaltiSicak1Id");

            migrationBuilder.CreateIndex(
                name: "IX_YemekMenuleri_KahvaltiSicak2Id",
                table: "YemekMenuleri",
                column: "KahvaltiSicak2Id");

            migrationBuilder.CreateIndex(
                name: "IX_YemekMenuleri_YardimciYemekId",
                table: "YemekMenuleri",
                column: "YardimciYemekId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekTanimlari_KategoriId",
                table: "YemekTanimlari",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekYorumlari_KullaniciId",
                table: "YemekYorumlari",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekYorumlari_YemekMenuId",
                table: "YemekYorumlari",
                column: "YemekMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatGrupUyeleri");

            migrationBuilder.DropTable(
                name: "Duyurular");

            migrationBuilder.DropTable(
                name: "EtkinlikKatilimcilari");

            migrationBuilder.DropTable(
                name: "EtkinlikYorumlari");

            migrationBuilder.DropTable(
                name: "GunlukYoklamalar");

            migrationBuilder.DropTable(
                name: "MaliIslemler");

            migrationBuilder.DropTable(
                name: "Mesajlar");

            migrationBuilder.DropTable(
                name: "OgrenciSohbetGruplari");

            migrationBuilder.DropTable(
                name: "PersonelNobetleri");

            migrationBuilder.DropTable(
                name: "Sikayetler");

            migrationBuilder.DropTable(
                name: "SohbetYoklamalar");

            migrationBuilder.DropTable(
                name: "StandartKahvaltiUrunleri");

            migrationBuilder.DropTable(
                name: "YemekYorumlari");

            migrationBuilder.DropTable(
                name: "Etkinlikler");

            migrationBuilder.DropTable(
                name: "YoklamaTurleri");

            migrationBuilder.DropTable(
                name: "GelirGiderYonu");

            migrationBuilder.DropTable(
                name: "ChatGruplari");

            migrationBuilder.DropTable(
                name: "Sohbetler");

            migrationBuilder.DropTable(
                name: "YemekMenuleri");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "SohbetGruplari");

            migrationBuilder.DropTable(
                name: "YemekTanimlari");

            migrationBuilder.DropTable(
                name: "Odalar");

            migrationBuilder.DropTable(
                name: "Roller");

            migrationBuilder.DropTable(
                name: "YemekKategorileri");
        }
    }
}
