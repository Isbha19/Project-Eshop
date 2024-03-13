﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class discountsavesChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "savedPrice",
                table: "shoppingCarts");

            migrationBuilder.AddColumn<double>(
                name: "savedPrice",
                table: "orderHeaders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "savedPrice",
                table: "orderHeaders");

            migrationBuilder.AddColumn<double>(
                name: "savedPrice",
                table: "shoppingCarts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
