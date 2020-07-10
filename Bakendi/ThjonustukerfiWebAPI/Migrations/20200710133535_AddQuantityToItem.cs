using Microsoft.EntityFrameworkCore.Migrations;

namespace ThjonustukerfiWebAPI.Migrations
{
    public partial class AddQuantityToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Item",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Item");
        }
    }
}
