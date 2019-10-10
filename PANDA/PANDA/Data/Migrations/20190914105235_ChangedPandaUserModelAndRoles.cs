using Microsoft.EntityFrameworkCore.Migrations;

namespace PANDA.Data.Migrations
{
    public partial class ChangedPandaUserModelAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_PandaUserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_PandaUserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "PandaUserId",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "Packages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PackageStatus",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_StatusId",
                table: "Packages",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageStatus_StatusId",
                table: "Packages",
                column: "StatusId",
                principalTable: "PackageStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageStatus_StatusId",
                table: "Packages");

            migrationBuilder.DropTable(
                name: "PackageStatus");

            migrationBuilder.DropIndex(
                name: "IX_Packages_StatusId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

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
    }
}
