using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScreenBin.Migrations
{
    public partial class Screenshots_ScreenshotTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Screenshots",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 191, nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    RelativePath = table.Column<string>(maxLength: 300, nullable: true),
                    UploadedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenshots", x => x.Id);
                }); ;
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Screenshots");
        }
    }
}
