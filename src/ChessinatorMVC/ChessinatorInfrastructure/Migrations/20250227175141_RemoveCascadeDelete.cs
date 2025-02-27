using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Title_TitleId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Player_PlayerId",
                table: "PlayerTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Tournament_TournamentId",
                table: "PlayerTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_Organizer_OrganizerId",
                table: "Tournament");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_TimeControl_TimeControlId",
                table: "Tournament");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Title_TitleId",
                table: "Player",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Player_PlayerId",
                table: "PlayerTournament",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Tournament_TournamentId",
                table: "PlayerTournament",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_Organizer_OrganizerId",
                table: "Tournament",
                column: "OrganizerId",
                principalTable: "Organizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_TimeControl_TimeControlId",
                table: "Tournament",
                column: "TimeControlId",
                principalTable: "TimeControl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Title_TitleId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Player_PlayerId",
                table: "PlayerTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Tournament_TournamentId",
                table: "PlayerTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_Organizer_OrganizerId",
                table: "Tournament");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_TimeControl_TimeControlId",
                table: "Tournament");



            migrationBuilder.AddForeignKey(
                name: "FK_Player_Title_TitleId",
                table: "Player",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Player_PlayerId",
                table: "PlayerTournament",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Tournament_TournamentId",
                table: "PlayerTournament",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_Organizer_OrganizerId",
                table: "Tournament",
                column: "OrganizerId",
                principalTable: "Organizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_TimeControl_TimeControlId",
                table: "Tournament",
                column: "TimeControlId",
                principalTable: "TimeControl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
