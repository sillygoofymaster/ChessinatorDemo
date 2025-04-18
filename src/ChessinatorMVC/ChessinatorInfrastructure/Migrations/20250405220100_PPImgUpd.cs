using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PPImgUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "TournamentPicturePath",
                table: "Tournament",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Organizer",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentPicturePath",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Organizer");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
