using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class shipCharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ShippingCharge",
                table: "orderHeaders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCharge",
                table: "orderHeaders");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 22, 9, 7, 59, 497, DateTimeKind.Local).AddTicks(4891), new DateTime(2024, 2, 21, 9, 7, 59, 497, DateTimeKind.Local).AddTicks(4873) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 21, 9, 7, 59, 497, DateTimeKind.Local).AddTicks(4901), new DateTime(2024, 2, 21, 9, 7, 59, 497, DateTimeKind.Local).AddTicks(4901) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 7, 59, 497, DateTimeKind.Local).AddTicks(4904), new DateTime(2024, 2, 21, 9, 7, 59, 497, DateTimeKind.Local).AddTicks(4903) });
        }
    }
}
