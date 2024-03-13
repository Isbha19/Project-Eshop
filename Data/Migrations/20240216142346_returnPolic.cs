using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class returnPolic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnPolicyValid",
                table: "orderHeaders");

            migrationBuilder.AddColumn<bool>(
                name: "ReturnPolicyValid",
                table: "orderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnPolicyValid",
                table: "orderDetails");

            migrationBuilder.AddColumn<bool>(
                name: "ReturnPolicyValid",
                table: "orderHeaders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
