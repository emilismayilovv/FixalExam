using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINALEXAM.Migrations
{
    public partial class Orderchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AboutTeams_AboutTeamId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "AboutTeamId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AboutTeams_AboutTeamId",
                table: "Orders",
                column: "AboutTeamId",
                principalTable: "AboutTeams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AboutTeams_AboutTeamId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "AboutTeamId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AboutTeams_AboutTeamId",
                table: "Orders",
                column: "AboutTeamId",
                principalTable: "AboutTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
