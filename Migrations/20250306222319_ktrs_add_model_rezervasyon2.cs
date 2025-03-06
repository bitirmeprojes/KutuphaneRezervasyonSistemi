using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KTRS.Migrations
{
    /// <inheritdoc />
    public partial class ktrs_add_model_rezervasyon2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koltuklar_KoltukIndexViewModel_KoltukIndexViewModelId",
                table: "Koltuklar");

            migrationBuilder.DropTable(
                name: "KoltukIndexViewModel");

            migrationBuilder.RenameColumn(
                name: "KoltukIndexViewModelId",
                table: "Koltuklar",
                newName: "KatId");

            migrationBuilder.RenameIndex(
                name: "IX_Koltuklar_KoltukIndexViewModelId",
                table: "Koltuklar",
                newName: "IX_Koltuklar_KatId");

            migrationBuilder.CreateTable(
                name: "Katlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KatNo = table.Column<int>(type: "integer", nullable: false),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    MaxRow = table.Column<int>(type: "integer", nullable: false),
                    MaxCol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Katlar", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Koltuklar_Katlar_KatId",
                table: "Koltuklar",
                column: "KatId",
                principalTable: "Katlar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koltuklar_Katlar_KatId",
                table: "Koltuklar");

            migrationBuilder.DropTable(
                name: "Katlar");

            migrationBuilder.RenameColumn(
                name: "KatId",
                table: "Koltuklar",
                newName: "KoltukIndexViewModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Koltuklar_KatId",
                table: "Koltuklar",
                newName: "IX_Koltuklar_KoltukIndexViewModelId");

            migrationBuilder.CreateTable(
                name: "KoltukIndexViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KatNo = table.Column<int>(type: "integer", nullable: false),
                    MaxCol = table.Column<int>(type: "integer", nullable: false),
                    MaxRow = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KoltukIndexViewModel", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Koltuklar_KoltukIndexViewModel_KoltukIndexViewModelId",
                table: "Koltuklar",
                column: "KoltukIndexViewModelId",
                principalTable: "KoltukIndexViewModel",
                principalColumn: "Id");
        }
    }
}
