using Microsoft.EntityFrameworkCore.Migrations;

namespace ThjonustukerfiWebAPI.Migrations
{
    public partial class typeIsNowStoredAsIdInItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Item");

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Item",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Item",
                type: "text",
                nullable: true);
        }
    }
}
