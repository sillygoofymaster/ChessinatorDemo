using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeControlTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TimeControl");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "TimeControl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TimeControlTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeControlTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeControl_TypeId",
                table: "TimeControl",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeControl_TimeControlTypes_TypeId",
                table: "TimeControl",
                column: "TypeId",
                principalTable: "TimeControlTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeControl_TimeControlTypes_TypeId",
                table: "TimeControl");

            migrationBuilder.DropTable(
                name: "TimeControlTypes");

            migrationBuilder.DropIndex(
                name: "IX_TimeControl_TypeId",
                table: "TimeControl");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "TimeControl");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "TimeControl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
