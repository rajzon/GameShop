using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Infrastructure.Migrations
{
    public partial class OrderWithOrderStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Address = table.Column<string>(maxLength: 80, nullable: false),
                    Street = table.Column<string>(maxLength: 80, nullable: false),
                    PostCode = table.Column<string>(maxLength: 6, nullable: false),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStocks",
                columns: table => new
                {
                    StockId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStocks", x => new { x.StockId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderStocks_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderStocks_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderStocks_OrderId",
                table: "OrderStocks",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStocks");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
