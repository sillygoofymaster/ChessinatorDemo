using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MatchFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConcluded",
                table: "ChessMatch");

            migrationBuilder.RenameColumn(
                name: "ConcludedByWhite",
                table: "ChessMatch",
                newName: "WhiteOpening");

            migrationBuilder.RenameColumn(
                name: "ConcludedByBlack",
                table: "ChessMatch",
                newName: "BlackOpening");

            migrationBuilder.AddColumn<int>(
                name: "BlackElo",
                table: "ChessMatch",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WhiteElo",
                table: "ChessMatch",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackElo",
                table: "ChessMatch");

            migrationBuilder.DropColumn(
                name: "WhiteElo",
                table: "ChessMatch");

            migrationBuilder.RenameColumn(
                name: "WhiteOpening",
                table: "ChessMatch",
                newName: "ConcludedByWhite");

            migrationBuilder.RenameColumn(
                name: "BlackOpening",
                table: "ChessMatch",
                newName: "ConcludedByBlack");

            migrationBuilder.AddColumn<int>(
                name: "IsConcluded",
                table: "ChessMatch",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
