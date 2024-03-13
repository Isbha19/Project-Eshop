using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class shipp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ShippingCharge",
                table: "shoppingCarts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 27, 12, 38, 15, 587, DateTimeKind.Local).AddTicks(6909), new DateTime(2024, 2, 26, 12, 38, 15, 587, DateTimeKind.Local).AddTicks(6893) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 26, 12, 38, 15, 587, DateTimeKind.Local).AddTicks(6918), new DateTime(2024, 2, 26, 12, 38, 15, 587, DateTimeKind.Local).AddTicks(6917) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 26, 12, 38, 15, 587, DateTimeKind.Local).AddTicks(6920), new DateTime(2024, 2, 26, 12, 38, 15, 587, DateTimeKind.Local).AddTicks(6919) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCharge",
                table: "shoppingCarts");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 27, 12, 27, 8, 554, DateTimeKind.Local).AddTicks(7303), new DateTime(2024, 2, 26, 12, 27, 8, 554, DateTimeKind.Local).AddTicks(7286) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 26, 12, 27, 8, 554, DateTimeKind.Local).AddTicks(7312), new DateTime(2024, 2, 26, 12, 27, 8, 554, DateTimeKind.Local).AddTicks(7312) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 26, 12, 27, 8, 554, DateTimeKind.Local).AddTicks(7314), new DateTime(2024, 2, 26, 12, 27, 8, 554, DateTimeKind.Local).AddTicks(7314) });
        }
    }
}
