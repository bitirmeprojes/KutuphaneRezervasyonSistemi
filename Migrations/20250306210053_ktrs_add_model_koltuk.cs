using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KTRS.Migrations
{
    /// <inheritdoc />
    public partial class ktrs_add_model_koltuk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "Koltuklar",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColumnIndex",
                table: "Koltuklar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KatNo",
                table: "Koltuklar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KoltukIndexViewModelId",
                table: "Koltuklar",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowIndex",
                table: "Koltuklar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KoltukIndexViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KatNo = table.Column<int>(type: "integer", nullable: false),
                    MaxRow = table.Column<int>(type: "integer", nullable: false),
                    MaxCol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KoltukIndexViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Koltuklar_KoltukIndexViewModelId",
                table: "Koltuklar",
                column: "KoltukIndexViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Koltuklar_KoltukIndexViewModel_KoltukIndexViewModelId",
                table: "Koltuklar",
                column: "KoltukIndexViewModelId",
                principalTable: "KoltukIndexViewModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koltuklar_KoltukIndexViewModel_KoltukIndexViewModelId",
                table: "Koltuklar");

            migrationBuilder.DropTable(
                name: "KoltukIndexViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Koltuklar_KoltukIndexViewModelId",
                table: "Koltuklar");

            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "Koltuklar");

            migrationBuilder.DropColumn(
                name: "ColumnIndex",
                table: "Koltuklar");

            migrationBuilder.DropColumn(
                name: "KatNo",
                table: "Koltuklar");

            migrationBuilder.DropColumn(
                name: "KoltukIndexViewModelId",
                table: "Koltuklar");

            migrationBuilder.DropColumn(
                name: "RowIndex",
                table: "Koltuklar");
        }
    }
}
