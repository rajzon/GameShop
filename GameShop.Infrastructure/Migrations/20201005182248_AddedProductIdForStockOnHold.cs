using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Infrastructure.Migrations
{
    public partial class AddedProductIdForStockOnHold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "StockOnHolds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "StockOnHolds");
        }
    }
}
