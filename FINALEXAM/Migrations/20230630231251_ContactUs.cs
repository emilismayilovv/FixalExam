using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINALEXAM.Migrations
{
    public partial class ContactUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeProperties_AboutTeams_AboutTeamId",
                table: "HomeProperties");

            migrationBuilder.AlterColumn<int>(
                name: "AboutTeamId",
                table: "HomeProperties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HomeProperties_AboutTeams_AboutTeamId",
                table: "HomeProperties",
                column: "AboutTeamId",
                principalTable: "AboutTeams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeProperties_AboutTeams_AboutTeamId",
                table: "HomeProperties");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.AlterColumn<int>(
                name: "AboutTeamId",
                table: "HomeProperties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeProperties_AboutTeams_AboutTeamId",
                table: "HomeProperties",
                column: "AboutTeamId",
                principalTable: "AboutTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
