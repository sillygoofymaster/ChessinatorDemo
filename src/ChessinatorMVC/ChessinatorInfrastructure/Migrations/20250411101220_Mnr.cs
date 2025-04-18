using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mnr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessMatch_Round_RoundId",
                table: "ChessMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_Round_Tournament_TournamentId",
                table: "Round");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Round",
                table: "Round");

            migrationBuilder.RenameTable(
                name: "Round",
                newName: "Rounds");

            migrationBuilder.RenameIndex(
                name: "IX_Round_TournamentId",
                table: "Rounds",
                newName: "IX_Rounds_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessMatch_Rounds_RoundId",
                table: "ChessMatch",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Tournament_TournamentId",
                table: "Rounds",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessMatch_Rounds_RoundId",
                table: "ChessMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Tournament_TournamentId",
                table: "Rounds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds");

            migrationBuilder.RenameTable(
                name: "Rounds",
                newName: "Round");

            migrationBuilder.RenameIndex(
                name: "IX_Rounds_TournamentId",
                table: "Round",
                newName: "IX_Round_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Round",
                table: "Round",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessMatch_Round_RoundId",
                table: "ChessMatch",
                column: "RoundId",
                principalTable: "Round",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Round_Tournament_TournamentId",
                table: "Round",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
