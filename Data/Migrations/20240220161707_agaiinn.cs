using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class agaiinn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "discountSavedPrice",
                table: "orderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 21, 21, 47, 6, 925, DateTimeKind.Local).AddTicks(5172), new DateTime(2024, 2, 20, 21, 47, 6, 925, DateTimeKind.Local).AddTicks(5153) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 20, 21, 47, 6, 925, DateTimeKind.Local).AddTicks(5183), new DateTime(2024, 2, 20, 21, 47, 6, 925, DateTimeKind.Local).AddTicks(5182) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 20, 21, 47, 6, 925, DateTimeKind.Local).AddTicks(5185), new DateTime(2024, 2, 20, 21, 47, 6, 925, DateTimeKind.Local).AddTicks(5185) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discountSavedPrice",
                table: "orderDetails");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 21, 21, 24, 25, 75, DateTimeKind.Local).AddTicks(2737), new DateTime(2024, 2, 20, 21, 24, 25, 75, DateTimeKind.Local).AddTicks(2721) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 20, 21, 24, 25, 75, DateTimeKind.Local).AddTicks(2746), new DateTime(2024, 2, 20, 21, 24, 25, 75, DateTimeKind.Local).AddTicks(2745) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 20, 21, 24, 25, 75, DateTimeKind.Local).AddTicks(2748), new DateTime(2024, 2, 20, 21, 24, 25, 75, DateTimeKind.Local).AddTicks(2748) });
        }
    }
}
