using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarbageCollection.Migrations
{
    public partial class ChangeIdBin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdBin",
                table: "BinCitizens",
                newName: "IdBin_Old");

            migrationBuilder.AddColumn<string>(
                name: "IdBin",
                table: "BinCitizens",
                type: "TEXT",
                nullable: true);

            migrationBuilder.Sql(
                "UPDATE BinCitizens SET IdBin = CAST(IdBin_Old AS TEXT)");

            migrationBuilder.DropColumn(
                name: "IdBin_Old",
                table: "BinCitizens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "IdBin_Old",
                table: "BinCitizens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(
                "UPDATE BinCitizens SET IdBin_Old = CAST(IdBin AS INTEGER)");

            migrationBuilder.DropColumn(
                name: "IdBin",
                table: "BinCitizens");

            migrationBuilder.RenameColumn(
                name: "IdBin_Old",
                table: "BinCitizens",
                newName: "IdBin");
        }
    }
}
