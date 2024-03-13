using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class offered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Offered",
                table: "shoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9366), new DateTime(2024, 2, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9351) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9379), new DateTime(2024, 2, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9379) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9381), new DateTime(2024, 2, 19, 8, 24, 8, 611, DateTimeKind.Local).AddTicks(9381) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Offered",
                table: "shoppingCarts");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 7, 46, 43, 19, DateTimeKind.Local).AddTicks(3917), new DateTime(2024, 2, 19, 7, 46, 43, 19, DateTimeKind.Local).AddTicks(3903) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 7, 46, 43, 19, DateTimeKind.Local).AddTicks(3926), new DateTime(2024, 2, 19, 7, 46, 43, 19, DateTimeKind.Local).AddTicks(3926) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 7, 46, 43, 19, DateTimeKind.Local).AddTicks(3928), new DateTime(2024, 2, 19, 7, 46, 43, 19, DateTimeKind.Local).AddTicks(3928) });
        }
    }
}
