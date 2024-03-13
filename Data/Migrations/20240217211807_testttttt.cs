using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class testttttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_wallets_WalletId",
                table: "wallets",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_wallets_walletsHeader_WalletId",
                table: "wallets",
                column: "WalletId",
                principalTable: "walletsHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wallets_walletsHeader_WalletId",
                table: "wallets");

            migrationBuilder.DropIndex(
                name: "IX_wallets_WalletId",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "wallets");
        }
    }
}
