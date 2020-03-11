using Microsoft.EntityFrameworkCore.Migrations;

namespace ThjonustukerfiWebAPI.Migrations
{
    public partial class UpdatededNameOfExceptionLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EceptionLog",
                table: "EceptionLog");

            migrationBuilder.RenameTable(
                name: "EceptionLog",
                newName: "ExceptionLog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExceptionLog",
                table: "ExceptionLog",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExceptionLog",
                table: "ExceptionLog");

            migrationBuilder.RenameTable(
                name: "ExceptionLog",
                newName: "EceptionLog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EceptionLog",
                table: "EceptionLog",
                column: "Id");
        }
    }
}
