using Microsoft.EntityFrameworkCore.Migrations;

namespace PANDA.Data.Migrations
{
    public partial class Added_PackageStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageStatus_StatusId",
                table: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackageStatus",
                table: "PackageStatus");

            migrationBuilder.RenameTable(
                name: "PackageStatus",
                newName: "PackageStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackageStatuses",
                table: "PackageStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageStatuses_StatusId",
                table: "Packages",
                column: "StatusId",
                principalTable: "PackageStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageStatuses_StatusId",
                table: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackageStatuses",
                table: "PackageStatuses");

            migrationBuilder.RenameTable(
                name: "PackageStatuses",
                newName: "PackageStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackageStatus",
                table: "PackageStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageStatus_StatusId",
                table: "Packages",
                column: "StatusId",
                principalTable: "PackageStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
