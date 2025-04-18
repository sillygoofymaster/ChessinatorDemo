using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnotherfTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ChessMatch");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "ChessMatch");

            migrationBuilder.RenameColumn(
                name: "RoundNumber",
                table: "ChessMatch",
                newName: "IsConcluded");

            migrationBuilder.AddColumn<string>(
                name: "ConcludedByBlack",
                table: "ChessMatch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcludedByWhite",
                table: "ChessMatch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoundId",
                table: "ChessMatch",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Round",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Round", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Round_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChessMatch_RoundId",
                table: "ChessMatch",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_TournamentId",
                table: "Round",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessMatch_Round_RoundId",
                table: "ChessMatch",
                column: "RoundId",
                principalTable: "Round",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessMatch_Round_RoundId",
                table: "ChessMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch");

            migrationBuilder.DropTable(
                name: "Round");

            migrationBuilder.DropIndex(
                name: "IX_ChessMatch_RoundId",
                table: "ChessMatch");

            migrationBuilder.DropColumn(
                name: "ConcludedByBlack",
                table: "ChessMatch");

            migrationBuilder.DropColumn(
                name: "ConcludedByWhite",
                table: "ChessMatch");

            migrationBuilder.DropColumn(
                name: "RoundId",
                table: "ChessMatch");

            migrationBuilder.RenameColumn(
                name: "IsConcluded",
                table: "ChessMatch",
                newName: "RoundNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "ChessMatch",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "ChessMatch",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ChessMatch_Tournament_TournamentId",
                table: "ChessMatch",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
