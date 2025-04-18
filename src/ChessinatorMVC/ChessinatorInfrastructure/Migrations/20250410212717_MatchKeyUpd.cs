using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MatchKeyUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch");

            migrationBuilder.DropIndex(
                name: "IX_ChessMatch_TournamentId",
                table: "ChessMatch");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "ChessMatch");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "ChessMatch",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChessMatch_TournamentId",
                table: "ChessMatch",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id");
        }
    }
}
