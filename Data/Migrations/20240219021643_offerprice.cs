using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class offerprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OfferPrice",
                table: "shoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferPrice",
                table: "shoppingCarts");

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
    }
}
