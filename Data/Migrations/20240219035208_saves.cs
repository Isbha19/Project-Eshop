using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class saves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Offered",
                table: "shoppingCarts");

            migrationBuilder.AddColumn<double>(
                name: "DiscountAmountApplied",
                table: "orderHeaders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2697), new DateTime(2024, 2, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2682) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2706), new DateTime(2024, 2, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2705) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2708), new DateTime(2024, 2, 19, 9, 22, 7, 981, DateTimeKind.Local).AddTicks(2707) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmountApplied",
                table: "orderHeaders");

            migrationBuilder.AddColumn<bool>(
                name: "Offered",
                table: "shoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9366), new DateTime(2024, 2, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9351) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9379), new DateTime(2024, 2, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9379) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9381), new DateTime(2024, 2, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9381) });
        }
    }
}
