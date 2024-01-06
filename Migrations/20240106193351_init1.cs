using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizyy_wpf.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlashCards",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    concept = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    definition = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    difficultylvl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashCards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Writes",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    question = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    answer = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    incorrectans1 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    incorrectans2 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    incorrectans3 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    difficultylvl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writes", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashCards");

            migrationBuilder.DropTable(
                name: "Writes");
        }
    }
}
