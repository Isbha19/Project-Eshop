using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class WalletHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "wallets");

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WalletHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletHeader_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wallets_WalletId",
                table: "wallets",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletHeader_UserId",
                table: "WalletHeader",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_wallets_WalletHeader_WalletId",
                table: "wallets",
                column: "WalletId",
                principalTable: "WalletHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wallets_WalletHeader_WalletId",
                table: "wallets");

            migrationBuilder.DropTable(
                name: "WalletHeader");

            migrationBuilder.DropIndex(
                name: "IX_wallets_WalletId",
                table: "wallets");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "wallets");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "wallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
