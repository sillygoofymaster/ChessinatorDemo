using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AndNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Winrate",
                table: "Player");

            migrationBuilder.AlterColumn<int>(
     name: "TotalGamesCount",
     table: "Player",
     type: "int",
     nullable: true,
     defaultValue: 0,
     oldClrType: typeof(int),
     oldType: "int",
     oldNullable: true,
     oldComputedColumnSql: "[Wins] + [Loses] + [Draws]");

            migrationBuilder.DropColumn(
                name: "Draws",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Loses",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Organizer",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Organizer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Organizer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Organizer");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Organizer");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Organizer",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Draws",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Loses",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TotalGamesCount",
                table: "Player",
                type: "int",
                nullable: true,
                computedColumnSql: "[Wins] + [Loses] + [Draws]",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "[FirstName] + ' ' + [LastName]");

            migrationBuilder.AddColumn<double>(
                name: "Winrate",
                table: "Player",
                type: "float",
                nullable: true,
                computedColumnSql: "CASE WHEN ([Wins] + [Loses] + [Draws]) = 0 THEN 0 ELSE CAST([Wins] AS FLOAT) / CAST([Wins] + [Loses] + [Draws] AS FLOAT) END");
        }
    }
}
