using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheatricalPlayersRefactoringKata.Migrations
{
    /// <inheritdoc />
    public partial class Insert_MockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlayTypes",
                columns: new[] { "Name", "Description", "DtInclusao" },
                values: new object[,]
                {
                    { "comedy", null, new DateTime(2024, 8, 22, 20, 58, 24, 570, DateTimeKind.Local).AddTicks(3692) },
                    { "history", "new genre", new DateTime(2024, 8, 22, 20, 58, 24, 570, DateTimeKind.Local).AddTicks(3693) },
                    { "tragedy", null, new DateTime(2024, 8, 22, 20, 58, 24, 570, DateTimeKind.Local).AddTicks(3682) }
                });

            migrationBuilder.InsertData(
                table: "Plays",
                columns: new[] { "Name", "Lines", "Type" },
                values: new object[,]
                {
                    { "As You Like It", 2670, "comedy" },
                    { "Hamlet", 4024, "tragedy" },
                    { "Henry V", 3227, "history" },
                    { "King John", 2648, "history" },
                    { "Othello", 3560, "tragedy" },
                    { "Richard III", 3718, "history" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plays",
                keyColumn: "Name",
                keyValue: "As You Like It");

            migrationBuilder.DeleteData(
                table: "Plays",
                keyColumn: "Name",
                keyValue: "Hamlet");

            migrationBuilder.DeleteData(
                table: "Plays",
                keyColumn: "Name",
                keyValue: "Henry V");

            migrationBuilder.DeleteData(
                table: "Plays",
                keyColumn: "Name",
                keyValue: "King John");

            migrationBuilder.DeleteData(
                table: "Plays",
                keyColumn: "Name",
                keyValue: "Othello");

            migrationBuilder.DeleteData(
                table: "Plays",
                keyColumn: "Name",
                keyValue: "Richard III");

            migrationBuilder.DeleteData(
                table: "PlayTypes",
                keyColumn: "Name",
                keyValue: "comedy");

            migrationBuilder.DeleteData(
                table: "PlayTypes",
                keyColumn: "Name",
                keyValue: "history");

            migrationBuilder.DeleteData(
                table: "PlayTypes",
                keyColumn: "Name",
                keyValue: "tragedy");
        }
    }
}
