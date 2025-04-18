using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackOpening",
                table: "ChessMatch");

            migrationBuilder.RenameColumn(
                name: "WhiteOpening",
                table: "ChessMatch",
                newName: "Opening");

            migrationBuilder.CreateTable(
                name: "Openings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moves = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Openings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Openings");

            migrationBuilder.RenameColumn(
                name: "Opening",
                table: "ChessMatch",
                newName: "WhiteOpening");

            migrationBuilder.AddColumn<string>(
                name: "BlackOpening",
                table: "ChessMatch",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
