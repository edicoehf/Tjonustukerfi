using Microsoft.EntityFrameworkCore.Migrations;

namespace ThjonustukerfiWebAPI.Migrations
{
    public partial class typoInTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemTimeStamp",
                table: "ItemTimeStamp");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ItemTimeStamp");

            migrationBuilder.RenameTable(
                name: "ItemTimeStamp",
                newName: "ItemTimestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemTimestamp",
                table: "ItemTimestamp",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemTimestamp",
                table: "ItemTimestamp");

            migrationBuilder.RenameTable(
                name: "ItemTimestamp",
                newName: "ItemTimeStamp");

            migrationBuilder.AddColumn<long>(
                name: "ItemId",
                table: "ItemTimeStamp",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemTimeStamp",
                table: "ItemTimeStamp",
                column: "Id");
        }
    }
}
