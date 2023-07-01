using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FINALEXAM.Migrations
{
    public partial class createType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeTypeId",
                table: "HomeProperties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HomeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeProperties_HomeTypeId",
                table: "HomeProperties",
                column: "HomeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeProperties_HomeTypes_HomeTypeId",
                table: "HomeProperties",
                column: "HomeTypeId",
                principalTable: "HomeTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeProperties_HomeTypes_HomeTypeId",
                table: "HomeProperties");

            migrationBuilder.DropTable(
                name: "HomeTypes");

            migrationBuilder.DropIndex(
                name: "IX_HomeProperties_HomeTypeId",
                table: "HomeProperties");

            migrationBuilder.DropColumn(
                name: "HomeTypeId",
                table: "HomeProperties");
        }
    }
}
