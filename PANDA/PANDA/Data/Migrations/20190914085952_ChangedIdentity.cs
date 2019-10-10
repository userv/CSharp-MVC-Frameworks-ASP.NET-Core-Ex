using Microsoft.EntityFrameworkCore.Migrations;

namespace PANDA.Data.Migrations
{
    public partial class ChangedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PandaUserId",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_PandaUserId",
                table: "AspNetRoles",
                column: "PandaUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_PandaUserId",
                table: "AspNetRoles",
                column: "PandaUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_PandaUserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_PandaUserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "PandaUserId",
                table: "AspNetRoles");
        }
    }
}
