using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "offers",
                columns: new[] { "OfferId", "Discount", "EndDate", "IsActive", "OfferName", "StartDate", "offerType" },
                values: new object[,]
                {
                    { 1, 0m, new DateTime(2024, 3, 19, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9693), true, "Discount 1", new DateTime(2024, 2, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9679), 0 },
                    { 2, 0m, new DateTime(2024, 4, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9716), true, "Discount 2", new DateTime(2024, 2, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9716), 0 },
                    { 3, 0m, new DateTime(2024, 5, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9719), true, "Discount 3", new DateTime(2024, 2, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9718), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3);
        }
    }
}
