﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PANDA.Data.Migrations
{
    public partial class ChangedEstimatedDeliveryNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetRoles");
        }
    }
}
