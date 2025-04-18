using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PlayerAndOrgEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Player",
                newName: "Details");

            migrationBuilder.AddColumn<string>(
                name: "Detais",
                table: "Organizer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "Organizer",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detais",
                table: "Organizer");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "Organizer");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Player",
                newName: "Email");
        }
    }
}
