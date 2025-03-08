using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTournamentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tournament");

            migrationBuilder.AddColumn<int>(
                name: "TournamentTypeId",
                table: "Tournament",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TournamentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_TournamentTypeId",
                table: "Tournament",
                column: "TournamentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_TournamentTypes_TournamentTypeId",
                table: "Tournament",
                column: "TournamentTypeId",
                principalTable: "TournamentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_TournamentTypes_TournamentTypeId",
                table: "Tournament");

            migrationBuilder.DropTable(
                name: "TournamentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tournament_TournamentTypeId",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "TournamentTypeId",
                table: "Tournament");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Tournament",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
