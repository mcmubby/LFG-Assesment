using Microsoft.EntityFrameworkCore.Migrations;

namespace LATimesheet.Data.Migrations
{
    public partial class Clockhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TimeTrackers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TimeTrackers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockStatus",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTrackers_ApplicationUserId",
                table: "TimeTrackers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTrackers_AspNetUsers_ApplicationUserId",
                table: "TimeTrackers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_AspNetUsers_ApplicationUserId",
                table: "TimeTrackers");

            migrationBuilder.DropIndex(
                name: "IX_TimeTrackers_ApplicationUserId",
                table: "TimeTrackers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TimeTrackers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeTrackers");

            migrationBuilder.DropColumn(
                name: "ClockStatus",
                table: "AspNetUsers");
        }
    }
}
