using Microsoft.EntityFrameworkCore.Migrations;

namespace ThjonustukerfiWebAPI.Migrations
{
    public partial class updateArchiveTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "OrderArchive");

            migrationBuilder.DropColumn(
                name: "OrderSize",
                table: "OrderArchive");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ItemArchive");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ItemArchive");

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "OrderArchive",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "OrderArchive");

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "OrderArchive",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderSize",
                table: "OrderArchive",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "ItemArchive",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServiceId",
                table: "ItemArchive",
                type: "bigint",
                nullable: true);
        }
    }
}
