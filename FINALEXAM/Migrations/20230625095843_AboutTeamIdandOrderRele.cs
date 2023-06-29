using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINALEXAM.Migrations
{
    public partial class AboutTeamIdandOrderRele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AboutTeamId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AboutTeamId",
                table: "Orders",
                column: "AboutTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AboutTeams_AboutTeamId",
                table: "Orders",
                column: "AboutTeamId",
                principalTable: "AboutTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AboutTeams_AboutTeamId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AboutTeamId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AboutTeamId",
                table: "Orders");
        }
    }
}
