using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarbageCollection.Migrations
{
    public partial class ChangeBinCitizenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinCitizens_Bins_IdBin",
                table: "BinCitizens");

            migrationBuilder.DropIndex(
                name: "IX_BinCitizens_IdBin",
                table: "BinCitizens");

            migrationBuilder.DropColumn(
                name: "IdBin",
                table: "BinCitizens");

            migrationBuilder.AddColumn<string>(
                name: "CodeBin",
                table: "BinCitizens",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BinCitizens_CodeBin",
                table: "BinCitizens",
                column: "CodeBin");

            migrationBuilder.AddForeignKey(
                name: "FK_BinCitizens_Bins_CodeBin",
                table: "BinCitizens",
                column: "CodeBin",
                principalTable: "Bins",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinCitizens_Bins_CodeBin",
                table: "BinCitizens");

            migrationBuilder.DropIndex(
                name: "IX_BinCitizens_CodeBin",
                table: "BinCitizens");

            migrationBuilder.DropColumn(
                name: "CodeBin",
                table: "BinCitizens");

            migrationBuilder.AddColumn<int>(
                name: "IdBin",
                table: "BinCitizens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BinCitizens_IdBin",
                table: "BinCitizens",
                column: "IdBin");

            migrationBuilder.AddForeignKey(
                name: "FK_BinCitizens_Bins_IdBin",
                table: "BinCitizens",
                column: "IdBin",
                principalTable: "Bins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}