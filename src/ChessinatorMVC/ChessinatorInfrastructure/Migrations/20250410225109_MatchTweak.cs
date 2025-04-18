using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MatchTweak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlackPlayerElo",
                table: "ChessMatch",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WhitePlayerElo",
                table: "ChessMatch",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_Name",
                table: "Tournament",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tournament_Name",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "BlackPlayerElo",
                table: "ChessMatch");

            migrationBuilder.DropColumn(
                name: "WhitePlayerElo",
                table: "ChessMatch");
        }
    }
}
