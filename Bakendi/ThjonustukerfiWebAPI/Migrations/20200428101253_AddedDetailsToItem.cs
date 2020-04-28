using Microsoft.EntityFrameworkCore.Migrations;

namespace ThjonustukerfiWebAPI.Migrations
{
    public partial class AddedDetailsToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Item");
        }
    }
}
