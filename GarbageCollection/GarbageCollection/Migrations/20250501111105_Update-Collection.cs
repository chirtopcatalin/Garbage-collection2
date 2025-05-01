using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarbageCollection.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "car_number",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "latitude",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "longitude",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "IdBin",
                table: "BinCitizens",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "car_number",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "latitude",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "Collections");

            migrationBuilder.AlterColumn<int>(
                name: "IdBin",
                table: "BinCitizens",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
