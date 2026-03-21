using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HudayiPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDuyuruModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "GecerlilikTarihi",
                table: "Duyurular",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OlusturanKullaniciId",
                table: "Duyurular",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GecerlilikTarihi",
                table: "Duyurular");

            migrationBuilder.DropColumn(
                name: "OlusturanKullaniciId",
                table: "Duyurular");
        }
    }
}
