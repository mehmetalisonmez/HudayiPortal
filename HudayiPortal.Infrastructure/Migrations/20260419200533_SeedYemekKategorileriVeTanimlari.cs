using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HudayiPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedYemekKategorileriVeTanimlari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "YemekKategorileri",
                columns: new[] { "Id", "KategoriAdi", "OlusturulmaTarihi", "SilindiMi" },
                values: new object[,]
                {
                    { 101, "Çorba", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 102, "Ana Yemek", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 103, "Yardımcı Yemek", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 104, "Ekstra", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 105, "Kahvaltılık Sıcak", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false }
                });

            migrationBuilder.InsertData(
                table: "YemekTanimlari",
                columns: new[] { "Id", "Kalori", "KategoriId", "OlusturulmaTarihi", "ResimUrl", "SilindiMi", "YemekAdi" },
                values: new object[,]
                {
                    { 101, 150, 101, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Mercimek Çorbası" },
                    { 102, 130, 101, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Ezogelin Çorbası" },
                    { 103, 120, 101, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Domates Çorbası" },
                    { 104, 350, 102, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Kuru Fasulye" },
                    { 105, 400, 102, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Orman Kebabı" },
                    { 106, 320, 102, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Tavuk Sote" },
                    { 107, 380, 102, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Etli Nohut" },
                    { 108, 250, 103, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Pirinç Pilavı" },
                    { 109, 230, 103, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Bulgur Pilavı" },
                    { 110, 280, 103, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Makarna" },
                    { 111, 80, 104, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Cacık" },
                    { 112, 60, 104, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Mevsim Salata" },
                    { 113, 70, 104, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Ayran" },
                    { 114, 220, 105, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Menemen" },
                    { 115, 200, 105, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Sahanda Yumurta" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "YemekTanimlari",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "YemekKategorileri",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "YemekKategorileri",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "YemekKategorileri",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "YemekKategorileri",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "YemekKategorileri",
                keyColumn: "Id",
                keyValue: 105);
        }
    }
}
