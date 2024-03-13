using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class totall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "shoppingCarts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "shoppingCarts");

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
    }
}
