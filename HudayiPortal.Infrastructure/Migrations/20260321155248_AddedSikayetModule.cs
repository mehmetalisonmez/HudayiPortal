using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HudayiPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSikayetModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellenmeTarihi",
                table: "Sikayetler",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuncellenmeTarihi",
                table: "Sikayetler");
        }
    }
}
