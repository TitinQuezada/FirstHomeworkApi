using Microsoft.EntityFrameworkCore.Migrations;

namespace Boundaries.Persistence.Migrations
{
    public partial class add_user_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ApplicationUsers",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApplicationUsers");
        }
    }
}
