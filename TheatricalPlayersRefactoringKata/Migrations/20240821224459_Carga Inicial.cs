using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Migrations
{
    /// <inheritdoc />
    public partial class CargaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayTypes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Plays",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Lines = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plays", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Plays_PlayTypes_Type",
                        column: x => x.Type,
                        principalTable: "PlayTypes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatementLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DtInclusao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Costumer = table.Column<string>(type: "TEXT", nullable: true),
                    PlayId = table.Column<string>(type: "TEXT", nullable: false),
                    Audience = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatementLogs_Plays_PlayId",
                        column: x => x.PlayId,
                        principalTable: "Plays",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plays_Type",
                table: "Plays",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_StatementLogs_DtInclusao_PlayId_Costumer",
                table: "StatementLogs",
                columns: new[] { "DtInclusao", "PlayId", "Costumer" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatementLogs_PlayId",
                table: "StatementLogs",
                column: "PlayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatementLogs");

            migrationBuilder.DropTable(
                name: "Plays");

            migrationBuilder.DropTable(
                name: "PlayTypes");
        }
    }
}
