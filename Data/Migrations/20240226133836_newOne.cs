using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class newOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isShipped",
                table: "orderHeaders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 27, 19, 8, 35, 748, DateTimeKind.Local).AddTicks(5841), new DateTime(2024, 2, 26, 19, 8, 35, 748, DateTimeKind.Local).AddTicks(5824) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 26, 19, 8, 35, 748, DateTimeKind.Local).AddTicks(5851), new DateTime(2024, 2, 26, 19, 8, 35, 748, DateTimeKind.Local).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 26, 19, 8, 35, 748, DateTimeKind.Local).AddTicks(5854), new DateTime(2024, 2, 26, 19, 8, 35, 748, DateTimeKind.Local).AddTicks(5853) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isShipped",
                table: "orderHeaders");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 27, 16, 2, 45, 129, DateTimeKind.Local).AddTicks(7663), new DateTime(2024, 2, 26, 16, 2, 45, 129, DateTimeKind.Local).AddTicks(7648) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 26, 16, 2, 45, 129, DateTimeKind.Local).AddTicks(7672), new DateTime(2024, 2, 26, 16, 2, 45, 129, DateTimeKind.Local).AddTicks(7671) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 26, 16, 2, 45, 129, DateTimeKind.Local).AddTicks(7674), new DateTime(2024, 2, 26, 16, 2, 45, 129, DateTimeKind.Local).AddTicks(7673) });
        }
    }
}
