using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Infrastructure.Migrations
{
    public partial class AddedStockOnHold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockOnHolds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(nullable: false),
                    StockQty = table.Column<int>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    SessionId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOnHolds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockOnHolds_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockOnHolds_StockId",
                table: "StockOnHolds",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockOnHolds");
        }
    }
}
