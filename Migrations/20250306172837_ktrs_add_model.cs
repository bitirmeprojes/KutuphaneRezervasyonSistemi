using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KTRS.Migrations
{
    /// <inheritdoc />
    public partial class ktrs_add_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Koltuklar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Durum = table.Column<bool>(type: "boolean", nullable: false),
                    koltukNo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koltuklar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    ogrenciID = table.Column<string>(type: "text", nullable: false),
                    rezervasyonNo = table.Column<string>(type: "text", nullable: false),
                    cikisSaat = table.Column<int>(type: "integer", nullable: false),
                    girisDurumu = table.Column<bool>(type: "boolean", nullable: false),
                    koltukNo = table.Column<string>(type: "text", nullable: false),
                    QRokutmaDurumu = table.Column<bool>(type: "boolean", nullable: false),
                    randevuBaslangicSaat = table.Column<int>(type: "integer", nullable: false),
                    randevuBitisSaat = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.ogrenciID);
                });

            migrationBuilder.CreateTable(
                name: "Rezervasyonlar",
                columns: table => new
                {
                    RezervasyonID = table.Column<string>(type: "text", nullable: false),
                    Durum = table.Column<bool>(type: "boolean", nullable: false),
                    ogrenciID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervasyonlar", x => x.RezervasyonID);
                });

            migrationBuilder.CreateTable(
                name: "Yetkililer",
                columns: table => new
                {
                    YetkiliID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yetkililer", x => x.YetkiliID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Koltuklar");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Rezervasyonlar");

            migrationBuilder.DropTable(
                name: "Yetkililer");
        }
    }
}
