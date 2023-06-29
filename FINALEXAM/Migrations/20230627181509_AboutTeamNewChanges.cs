using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINALEXAM.Migrations
{
    public partial class AboutTeamNewChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "AboutTeams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "AboutTeams");
        }
    }
}
