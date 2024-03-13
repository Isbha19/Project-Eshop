using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class againWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWishlisted",
                table: "wishlists");

            migrationBuilder.AddColumn<bool>(
                name: "isWishlisted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isWishlisted",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsWishlisted",
                table: "wishlists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
