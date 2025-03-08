using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTRS.Migrations
{
    /// <inheritdoc />
    public partial class AddKoltukCoords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "XCoord",
                table: "Koltuklar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YCoord",
                table: "Koltuklar",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XCoord",
                table: "Koltuklar");

            migrationBuilder.DropColumn(
                name: "YCoord",
                table: "Koltuklar");
        }
    }
}
