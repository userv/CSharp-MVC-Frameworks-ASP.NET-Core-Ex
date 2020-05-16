using Microsoft.EntityFrameworkCore.Migrations;

namespace PANDA.Data.Migrations
{
    public partial class Added_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageStatuses_StatusId",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Packages",
                newName: "PackageStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_StatusId",
                table: "Packages",
                newName: "IX_Packages_PackageStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageStatuses_PackageStatusId",
                table: "Packages",
                column: "PackageStatusId",
                principalTable: "PackageStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageStatuses_PackageStatusId",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "PackageStatusId",
                table: "Packages",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_PackageStatusId",
                table: "Packages",
                newName: "IX_Packages_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageStatuses_StatusId",
                table: "Packages",
                column: "StatusId",
                principalTable: "PackageStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
