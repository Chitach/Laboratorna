using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab.Migrations
{
    public partial class FixPack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Tests",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "TestName",
                table: "Tests",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "Results",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "UpperBorder",
                table: "Demands",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "LowerBorder",
                table: "Demands",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Results");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Users",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Tests");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Tests",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "TestName",
                table: "Tests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpperBorder",
                table: "Demands",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "LowerBorder",
                table: "Demands",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserId",
                table: "Users",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
