using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Result",
                table: "ChessMatch",
                newName: "MatchResultId");

            migrationBuilder.CreateTable(
                name: "MatchResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Result = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChessMatch_MatchResultId",
                table: "ChessMatch",
                column: "MatchResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessMatch_MatchResults_MatchResultId",
                table: "ChessMatch",
                column: "MatchResultId",
                principalTable: "MatchResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessMatch_MatchResults_MatchResultId",
                table: "ChessMatch");

            migrationBuilder.DropTable(
                name: "MatchResults");

            migrationBuilder.DropIndex(
                name: "IX_ChessMatch_MatchResultId",
                table: "ChessMatch");

            migrationBuilder.RenameColumn(
                name: "MatchResultId",
                table: "ChessMatch",
                newName: "Result");
        }
    }
}
