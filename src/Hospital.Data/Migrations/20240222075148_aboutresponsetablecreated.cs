using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Data.Migrations
{
    public partial class aboutresponsetablecreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutsResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Response1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Response2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Response3 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description1 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description2 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description3 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutsResponse", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutsResponse");
        }
    }
}
