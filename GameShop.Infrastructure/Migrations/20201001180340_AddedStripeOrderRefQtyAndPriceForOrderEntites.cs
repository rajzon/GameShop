using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Infrastructure.Migrations
{
    public partial class AddedStripeOrderRefQtyAndPriceForOrderEntites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderStocks",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderStocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderPrice",
                table: "Orders",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderRef",
                table: "Orders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "StripeRef",
                table: "Orders",
                maxLength: 35,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderStocks");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderStocks");

            migrationBuilder.DropColumn(
                name: "OrderPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderRef",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StripeRef",
                table: "Orders");
        }
    }
}
