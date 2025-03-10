using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTRS.Migrations
{
    /// <inheritdoc />
    public partial class AddKoltukCoords2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnIndex",
                table: "Koltuklar");

            migrationBuilder.DropColumn(
                name: "RowIndex",
                table: "Koltuklar");

            migrationBuilder.DropColumn(
                name: "MaxCol",
                table: "Katlar");

            migrationBuilder.DropColumn(
                name: "MaxRow",
                table: "Katlar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColumnIndex",
                table: "Koltuklar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RowIndex",
                table: "Koltuklar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxCol",
                table: "Katlar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxRow",
                table: "Katlar",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
