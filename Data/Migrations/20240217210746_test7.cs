using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class test7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walletsHeader_AspNetUsers_UserId",
                table: "walletsHeader");

            migrationBuilder.DropIndex(
                name: "IX_walletsHeader_UserId",
                table: "walletsHeader");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "walletsHeader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "walletsHeader",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_walletsHeader_UserId",
                table: "walletsHeader",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_walletsHeader_AspNetUsers_UserId",
                table: "walletsHeader",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
