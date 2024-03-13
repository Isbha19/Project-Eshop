using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class offername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfferName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferName",
                table: "Products");

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
    }
}
