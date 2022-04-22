using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FTAAPI.Migrations
{
    public partial class addedratingtodestination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Destinations",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Destinations");
        }
    }
}
