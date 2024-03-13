using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class @double : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "OfferPrice",
                table: "shoppingCarts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "DiscountPrice",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "offers",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "Discount", "EndDate", "StartDate" },
                values: new object[] { 0.0, new DateTime(2024, 3, 20, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1954), new DateTime(2024, 2, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1932) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "Discount", "EndDate", "StartDate" },
                values: new object[] { 0.0, new DateTime(2024, 4, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1965), new DateTime(2024, 2, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1964) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "Discount", "EndDate", "StartDate" },
                values: new object[] { 0.0, new DateTime(2024, 5, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1967), new DateTime(2024, 2, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1967) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OfferPrice",
                table: "shoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "offers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "Discount", "EndDate", "StartDate" },
                values: new object[] { 0m, new DateTime(2024, 3, 20, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2697), new DateTime(2024, 2, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2682) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "Discount", "EndDate", "StartDate" },
                values: new object[] { 0m, new DateTime(2024, 4, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2706), new DateTime(2024, 2, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2705) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "Discount", "EndDate", "StartDate" },
                values: new object[] { 0m, new DateTime(2024, 5, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2708), new DateTime(2024, 2, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2707) });
        }
    }
}
