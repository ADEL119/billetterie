using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilletterieAPI.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lieux",
                columns: table => new
                {
                    LieuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lieux", x => x.LieuId);
                });

            migrationBuilder.CreateTable(
                name: "Spectateurs",
                columns: table => new
                {
                    SpectateurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spectateurs", x => x.SpectateurId);
                });

            migrationBuilder.CreateTable(
                name: "Evenements",
                columns: table => new
                {
                    EvenementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateEvenement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LieuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenements", x => x.EvenementId);
                    table.ForeignKey(
                        name: "FK_Evenements_Lieux_LieuId",
                        column: x => x.LieuId,
                        principalTable: "Lieux",
                        principalColumn: "LieuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeBillets",
                columns: table => new
                {
                    TypeBilletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Tarif = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantiteStock = table.Column<int>(type: "int", nullable: false),
                    EvenementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeBillets", x => x.TypeBilletId);
                    table.ForeignKey(
                        name: "FK_TypeBillets_Evenements_EvenementId",
                        column: x => x.EvenementId,
                        principalTable: "Evenements",
                        principalColumn: "EvenementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Achats",
                columns: table => new
                {
                    AchatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantiteAchetee = table.Column<int>(type: "int", nullable: false),
                    TypeBilletId = table.Column<int>(type: "int", nullable: false),
                    SpectateurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achats", x => x.AchatId);
                    table.ForeignKey(
                        name: "FK_Achats_Spectateurs_SpectateurId",
                        column: x => x.SpectateurId,
                        principalTable: "Spectateurs",
                        principalColumn: "SpectateurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Achats_TypeBillets_TypeBilletId",
                        column: x => x.TypeBilletId,
                        principalTable: "TypeBillets",
                        principalColumn: "TypeBilletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achats_SpectateurId",
                table: "Achats",
                column: "SpectateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Achats_TypeBilletId",
                table: "Achats",
                column: "TypeBilletId");

            migrationBuilder.CreateIndex(
                name: "IX_Evenements_LieuId",
                table: "Evenements",
                column: "LieuId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeBillets_EvenementId",
                table: "TypeBillets",
                column: "EvenementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achats");

            migrationBuilder.DropTable(
                name: "Spectateurs");

            migrationBuilder.DropTable(
                name: "TypeBillets");

            migrationBuilder.DropTable(
                name: "Evenements");

            migrationBuilder.DropTable(
                name: "Lieux");
        }
    }
}
