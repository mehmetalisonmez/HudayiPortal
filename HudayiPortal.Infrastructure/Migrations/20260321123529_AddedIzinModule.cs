using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HudayiPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIzinModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IzinTurleri",
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
                    table.PrimaryKey("PK_IzinTurleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Izinler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    IzinTurId = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GidecegiAdres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OnaylayanPersonelId = table.Column<int>(type: "int", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izinler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Izinler_IzinTurleri_IzinTurId",
                        column: x => x.IzinTurId,
                        principalTable: "IzinTurleri",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Izinler_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Izinler_Kullanicilar_OnaylayanPersonelId",
                        column: x => x.OnaylayanPersonelId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Izinler_IzinTurId",
                table: "Izinler",
                column: "IzinTurId");

            migrationBuilder.CreateIndex(
                name: "IX_Izinler_KullaniciId",
                table: "Izinler",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Izinler_OnaylayanPersonelId",
                table: "Izinler",
                column: "OnaylayanPersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Izinler");

            migrationBuilder.DropTable(
                name: "IzinTurleri");
        }
    }
}
