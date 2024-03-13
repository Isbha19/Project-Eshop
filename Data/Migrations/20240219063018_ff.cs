using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class ff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8213), new DateTime(2024, 2, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8197) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8226), new DateTime(2024, 2, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8225) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8229), new DateTime(2024, 2, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8228) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1954), new DateTime(2024, 2, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1932) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1965), new DateTime(2024, 2, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1964) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1967), new DateTime(2024, 2, 19, 10, 50, 12, 670, DateTimeKind.Local).AddTicks(1967) });
        }
    }
}
