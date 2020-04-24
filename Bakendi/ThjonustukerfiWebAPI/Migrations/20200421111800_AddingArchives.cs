using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ThjonustukerfiWebAPI.Migrations
{
    public partial class AddingArchives : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemArchive",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderArchiveId = table.Column<long>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    CategoryId = table.Column<long>(nullable: true),
                    Service = table.Column<string>(nullable: true),
                    ServiceId = table.Column<long>(nullable: true),
                    extraDataJSON = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateCompleted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemArchive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderArchive",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Customer = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: true),
                    JSON = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateCompleted = table.Column<DateTime>(nullable: false),
                    OrderSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderArchive", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemArchive");

            migrationBuilder.DropTable(
                name: "OrderArchive");
        }
    }
}
