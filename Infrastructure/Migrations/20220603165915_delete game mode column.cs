using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class deletegamemodecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStats_Quotes_QuizId",
                table: "GameStats");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_GameModes_GameModeId",
                table: "Quizzes");

            migrationBuilder.DropTable(
                name: "GameModes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_GameModeId",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "GameModeId",
                table: "Quizzes",
                newName: "GameMode");

            migrationBuilder.CreateIndex(
                name: "IX_GameStats_QuotId",
                table: "GameStats",
                column: "QuotId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStats_Quotes_QuotId",
                table: "GameStats",
                column: "QuotId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStats_Quotes_QuotId",
                table: "GameStats");

            migrationBuilder.DropIndex(
                name: "IX_GameStats_QuotId",
                table: "GameStats");

            migrationBuilder.RenameColumn(
                name: "GameMode",
                table: "Quizzes",
                newName: "GameModeId");

            migrationBuilder.CreateTable(
                name: "GameModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameModes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_GameModeId",
                table: "Quizzes",
                column: "GameModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStats_Quotes_QuizId",
                table: "GameStats",
                column: "QuizId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_GameModes_GameModeId",
                table: "Quizzes",
                column: "GameModeId",
                principalTable: "GameModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
