using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Infrastructure.Migrations
{
    public partial class EditedDeliveryOptsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DeliveryOpts",
                maxLength: 400,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DeliveryOpts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Odit, similique?");

            migrationBuilder.UpdateData(
                table: "DeliveryOpts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam pharetra, magna et blandit faucibus, tortor turpis euismod tellus, eu efficitur lorem ligula in ante.Maecenas finibus velit vel ante dictum sagittis. Morbi vitae eros tellus. Fusce mollis sit amet est sed accumsan.");

            migrationBuilder.UpdateData(
                table: "DeliveryOpts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Vivamus sed nulla quam. Aliquam sit amet risus hendrerit orci scelerisque pellentesque.");

            migrationBuilder.UpdateData(
                table: "DeliveryOpts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Usce mollis sit amet est sed accumsan. Proin vel quam vitae velit condimentum lobortis et ac diam.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DeliveryOpts");
        }
    }
}
