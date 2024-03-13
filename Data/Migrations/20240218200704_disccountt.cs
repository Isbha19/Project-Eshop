using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class disccountt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isDiscounted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 1, 37, 4, 31, DateTimeKind.Local).AddTicks(6608), new DateTime(2024, 2, 19, 1, 37, 4, 31, DateTimeKind.Local).AddTicks(6592) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 1, 37, 4, 31, DateTimeKind.Local).AddTicks(6618), new DateTime(2024, 2, 19, 1, 37, 4, 31, DateTimeKind.Local).AddTicks(6617) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 1, 37, 4, 31, DateTimeKind.Local).AddTicks(6620), new DateTime(2024, 2, 19, 1, 37, 4, 31, DateTimeKind.Local).AddTicks(6620) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "isDiscounted",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 0, 44, 42, 88, DateTimeKind.Local).AddTicks(7543), new DateTime(2024, 2, 19, 0, 44, 42, 88, DateTimeKind.Local).AddTicks(7499) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 0, 44, 42, 88, DateTimeKind.Local).AddTicks(7571), new DateTime(2024, 2, 19, 0, 44, 42, 88, DateTimeKind.Local).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 0, 44, 42, 88, DateTimeKind.Local).AddTicks(7574), new DateTime(2024, 2, 19, 0, 44, 42, 88, DateTimeKind.Local).AddTicks(7573) });
        }
    }
}
