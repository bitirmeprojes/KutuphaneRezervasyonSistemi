using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTRS.Migrations
{
    /// <inheritdoc />
    public partial class ktrs_add_model_rezervasyon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KoltukNo",
                table: "Rezervasyonlar",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OlusturmaTarihi",
                table: "Rezervasyonlar",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KoltukNo",
                table: "Rezervasyonlar");

            migrationBuilder.DropColumn(
                name: "OlusturmaTarihi",
                table: "Rezervasyonlar");
        }
    }
}
