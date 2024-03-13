using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class offe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDiscount",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 3, 40, 29, 328, DateTimeKind.Local).AddTicks(469), new DateTime(2024, 2, 19, 3, 40, 29, 328, DateTimeKind.Local).AddTicks(451) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 3, 40, 29, 328, DateTimeKind.Local).AddTicks(480), new DateTime(2024, 2, 19, 3, 40, 29, 328, DateTimeKind.Local).AddTicks(480) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 3, 40, 29, 328, DateTimeKind.Local).AddTicks(482), new DateTime(2024, 2, 19, 3, 40, 29, 328, DateTimeKind.Local).AddTicks(482) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDiscount",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 2, 5, 28, 258, DateTimeKind.Local).AddTicks(803), new DateTime(2024, 2, 19, 2, 5, 28, 258, DateTimeKind.Local).AddTicks(785) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 2, 5, 28, 258, DateTimeKind.Local).AddTicks(815), new DateTime(2024, 2, 19, 2, 5, 28, 258, DateTimeKind.Local).AddTicks(814) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 2, 5, 28, 258, DateTimeKind.Local).AddTicks(818), new DateTime(2024, 2, 19, 2, 5, 28, 258, DateTimeKind.Local).AddTicks(817) });
        }
    }
}
