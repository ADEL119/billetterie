using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilletterieAPI.Migrations
{
    /// <inheritdoc />
    public partial class IdentityProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spectateurs_AspNetUsers_UserId",
                table: "Spectateurs");

            migrationBuilder.DropIndex(
                name: "IX_Spectateurs_UserId",
                table: "Spectateurs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Spectateurs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Spectateurs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Spectateurs_UserId",
                table: "Spectateurs",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Spectateurs_AspNetUsers_UserId",
                table: "Spectateurs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
