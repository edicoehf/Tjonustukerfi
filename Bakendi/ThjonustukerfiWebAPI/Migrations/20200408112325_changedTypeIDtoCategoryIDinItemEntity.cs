using Microsoft.EntityFrameworkCore.Migrations;

namespace ThjonustukerfiWebAPI.Migrations
{
    public partial class changedTypeIDtoCategoryIDinItemEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Item");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Item",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Item");

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Item",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
