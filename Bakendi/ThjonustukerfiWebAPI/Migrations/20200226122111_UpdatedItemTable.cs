using Microsoft.EntityFrameworkCore.Migrations;

namespace ThjonustukerfiWebAPI.Migrations
{
    public partial class UpdatedItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Item");
        }
    }
}
