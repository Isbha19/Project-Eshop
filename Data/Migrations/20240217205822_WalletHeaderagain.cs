using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class WalletHeaderagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletHeader_AspNetUsers_UserId",
                table: "WalletHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_wallets_WalletHeader_WalletId",
                table: "wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletHeader",
                table: "WalletHeader");

            migrationBuilder.RenameTable(
                name: "WalletHeader",
                newName: "walletsHeader");

            migrationBuilder.RenameIndex(
                name: "IX_WalletHeader_UserId",
                table: "walletsHeader",
                newName: "IX_walletsHeader_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_walletsHeader",
                table: "walletsHeader",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_wallets_walletsHeader_WalletId",
                table: "wallets",
                column: "WalletId",
                principalTable: "walletsHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_walletsHeader_AspNetUsers_UserId",
                table: "walletsHeader",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wallets_walletsHeader_WalletId",
                table: "wallets");

            migrationBuilder.DropForeignKey(
                name: "FK_walletsHeader_AspNetUsers_UserId",
                table: "walletsHeader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_walletsHeader",
                table: "walletsHeader");

            migrationBuilder.RenameTable(
                name: "walletsHeader",
                newName: "WalletHeader");

            migrationBuilder.RenameIndex(
                name: "IX_walletsHeader_UserId",
                table: "WalletHeader",
                newName: "IX_WalletHeader_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletHeader",
                table: "WalletHeader",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletHeader_AspNetUsers_UserId",
                table: "WalletHeader",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wallets_WalletHeader_WalletId",
                table: "wallets",
                column: "WalletId",
                principalTable: "WalletHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
