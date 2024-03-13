using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class offereddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDiscounted",
                table: "Products",
                newName: "isOffered");

            migrationBuilder.RenameColumn(
                name: "DiscountPrice",
                table: "Products",
                newName: "OfferPrice");

            migrationBuilder.AlterColumn<string>(
                name: "OfferType",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OfferName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 21, 19, 57, 4, 329, DateTimeKind.Local).AddTicks(310), new DateTime(2024, 2, 20, 19, 57, 4, 329, DateTimeKind.Local).AddTicks(294) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 20, 19, 57, 4, 329, DateTimeKind.Local).AddTicks(320), new DateTime(2024, 2, 20, 19, 57, 4, 329, DateTimeKind.Local).AddTicks(319) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 20, 19, 57, 4, 329, DateTimeKind.Local).AddTicks(322), new DateTime(2024, 2, 20, 19, 57, 4, 329, DateTimeKind.Local).AddTicks(322) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isOffered",
                table: "Products",
                newName: "isDiscounted");

            migrationBuilder.RenameColumn(
                name: "OfferPrice",
                table: "Products",
                newName: "DiscountPrice");

            migrationBuilder.AlterColumn<string>(
                name: "OfferType",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OfferName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8213), new DateTime(2024, 2, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8197) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 4, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8226), new DateTime(2024, 2, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8225) });

            migrationBuilder.UpdateData(
                table: "offers",
                keyColumn: "OfferId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8229), new DateTime(2024, 2, 19, 12, 0, 17, 698, DateTimeKind.Local).AddTicks(8228) });
        }
    }
}
