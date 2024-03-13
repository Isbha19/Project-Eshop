using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class catgry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoryOffers_Products_ProductId",
                table: "categoryOffers");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "categoryOffers",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_categoryOffers_ProductId",
                table: "categoryOffers",
                newName: "IX_categoryOffers_CategoryId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_categoryOffers_Categories_CategoryId",
                table: "categoryOffers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoryOffers_Categories_CategoryId",
                table: "categoryOffers");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "categoryOffers",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_categoryOffers_CategoryId",
                table: "categoryOffers",
                newName: "IX_categoryOffers_ProductId");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 19, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9693), new DateTime(2024, 2, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9679) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9716), new DateTime(2024, 2, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9716) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9719), new DateTime(2024, 2, 18, 23, 10, 5, 193, DateTimeKind.Local).AddTicks(9718) });

            migrationBuilder.AddForeignKey(
                name: "FK_categoryOffers_Products_ProductId",
                table: "categoryOffers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
