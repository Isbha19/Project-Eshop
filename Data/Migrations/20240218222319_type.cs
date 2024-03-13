using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfferType",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 3, 53, 18, 121, DateTimeKind.Local).AddTicks(3039), new DateTime(2024, 2, 19, 3, 53, 18, 121, DateTimeKind.Local).AddTicks(3022) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 3, 53, 18, 121, DateTimeKind.Local).AddTicks(3049), new DateTime(2024, 2, 19, 3, 53, 18, 121, DateTimeKind.Local).AddTicks(3048) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 3, 53, 18, 121, DateTimeKind.Local).AddTicks(3051), new DateTime(2024, 2, 19, 3, 53, 18, 121, DateTimeKind.Local).AddTicks(3050) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferType",
                table: "Products");

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
    }
}
