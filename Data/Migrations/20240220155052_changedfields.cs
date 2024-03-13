using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountedPrice",
                table: "shoppingCarts",
                newName: "CouponDiscountPrice");

            migrationBuilder.RenameColumn(
                name: "DiscountApplied",
                table: "shoppingCarts",
                newName: "CouponApplied");

            migrationBuilder.RenameColumn(
                name: "DiscountedPrice",
                table: "orderDetails",
                newName: "CouponDiscountPrice");

            migrationBuilder.AlterColumn<double>(
                name: "Percentage",
                table: "coupons",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CouponDiscountPrice",
                table: "shoppingCarts",
                newName: "DiscountedPrice");

            migrationBuilder.RenameColumn(
                name: "CouponApplied",
                table: "shoppingCarts",
                newName: "DiscountApplied");

            migrationBuilder.RenameColumn(
                name: "CouponDiscountPrice",
                table: "orderDetails",
                newName: "DiscountedPrice");

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage",
                table: "coupons",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

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
    }
}
