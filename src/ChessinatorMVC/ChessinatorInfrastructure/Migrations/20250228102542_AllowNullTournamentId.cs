using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullTournamentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id");
        }
    }
}
