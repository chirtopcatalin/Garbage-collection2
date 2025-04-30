using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarbageCollection.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Cnp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeBin = table.Column<string>(nullable: true),
                    CollectionTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_Bins_CodeBin",
                        column: x => x.CodeBin,
                        principalTable: "Bins",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BinCitizens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdBin = table.Column<int>(nullable: false),
                    IdCitizen = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinCitizens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinCitizens_Bins_IdBin",
                        column: x => x.IdBin,
                        principalTable: "Bins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BinCitizens_Citizens_IdCitizen",
                        column: x => x.IdCitizen,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bins_Code",
                table: "Bins",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CodeBin",
                table: "Collections",
                column: "CodeBin");

            migrationBuilder.CreateIndex(
                name: "IX_BinCitizens_IdBin",
                table: "BinCitizens",
                column: "IdBin");

            migrationBuilder.CreateIndex(
                name: "IX_BinCitizens_IdCitizen",
                table: "BinCitizens",
                column: "IdCitizen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "BinCitizens");
            migrationBuilder.DropTable(name: "Collections");
            migrationBuilder.DropTable(name: "Citizens");
            migrationBuilder.DropTable(name: "Bins");
        }

    }
}
