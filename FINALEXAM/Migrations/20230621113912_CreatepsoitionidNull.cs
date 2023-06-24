using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINALEXAM.Migrations
{
    public partial class CreatepsoitionidNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutTeams_AboutPositions_AboutPositionId",
                table: "AboutTeams");

            migrationBuilder.AlterColumn<int>(
                name: "AboutPositionId",
                table: "AboutTeams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutTeams_AboutPositions_AboutPositionId",
                table: "AboutTeams",
                column: "AboutPositionId",
                principalTable: "AboutPositions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutTeams_AboutPositions_AboutPositionId",
                table: "AboutTeams");

            migrationBuilder.AlterColumn<int>(
                name: "AboutPositionId",
                table: "AboutTeams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AboutTeams_AboutPositions_AboutPositionId",
                table: "AboutTeams",
                column: "AboutPositionId",
                principalTable: "AboutPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
