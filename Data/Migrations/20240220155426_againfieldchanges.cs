using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class againfieldchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OfferPrice",
                table: "shoppingCarts",
                newName: "CartItemOfferPrice");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartItemOfferPrice",
                table: "shoppingCarts",
                newName: "OfferPrice");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 21, 21, 20, 50, 974, DateTimeKind.Local).AddTicks(1441), new DateTime(2024, 2, 20, 21, 20, 50, 974, DateTimeKind.Local).AddTicks(1425) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 20, 21, 20, 50, 974, DateTimeKind.Local).AddTicks(1450), new DateTime(2024, 2, 20, 21, 20, 50, 974, DateTimeKind.Local).AddTicks(1449) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 20, 21, 20, 50, 974, DateTimeKind.Local).AddTicks(1452), new DateTime(2024, 2, 20, 21, 20, 50, 974, DateTimeKind.Local).AddTicks(1452) });
        }
    }
}
