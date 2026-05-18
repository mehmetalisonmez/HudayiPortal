using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HudayiPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIslemKategorileriAndUpdateMaliIslem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BelgeUrl",
                table: "MaliIslemler",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KategoriId",
                table: "MaliIslemler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IslemKategorileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YonId = table.Column<int>(type: "int", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SilindiMi = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IslemKategorileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IslemKategorileri_GelirGiderYonu_YonId",
                        column: x => x.YonId,
                        principalTable: "GelirGiderYonu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaliIslemler_KategoriId",
                table: "MaliIslemler",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_IslemKategorileri_YonId",
                table: "IslemKategorileri",
                column: "YonId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaliIslemler_IslemKategorileri_KategoriId",
                table: "MaliIslemler",
                column: "KategoriId",
                principalTable: "IslemKategorileri",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaliIslemler_IslemKategorileri_KategoriId",
                table: "MaliIslemler");

            migrationBuilder.DropTable(
                name: "IslemKategorileri");

            migrationBuilder.DropIndex(
                name: "IX_MaliIslemler_KategoriId",
                table: "MaliIslemler");

            migrationBuilder.DropColumn(
                name: "BelgeUrl",
                table: "MaliIslemler");

            migrationBuilder.DropColumn(
                name: "KategoriId",
                table: "MaliIslemler");
        }
    }
}
