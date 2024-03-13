using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class originl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
