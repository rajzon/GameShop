using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Infrastructure.Migrations
{
    public partial class AddedDeliveryOptEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryOpts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    Currency = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOpts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DeliveryOpts",
                columns: new[] { "Id", "Currency", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "$", "DPD", 5m },
                    { 2, "$", "DHL", 4.55m },
                    { 3, "$", "INPOST", 5.20m },
                    { 4, "$", "DIGITAL_PRODUCT", 0m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryOpts");
        }
    }
}
