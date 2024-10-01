using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheatricalPlayersRefactoringKata.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedsPlays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Play",
                columns: new[] { "Id", "CreatedAt", "Lines", "Name", "TypeGenreId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 1, 11, 31, 13, 714, DateTimeKind.Local).AddTicks(2714), 4024, "Hamlet", 1 },
                    { 2, new DateTime(2024, 10, 1, 11, 31, 13, 714, DateTimeKind.Local).AddTicks(2724), 2670, "As You Like It", 2 },
                    { 3, new DateTime(2024, 10, 1, 11, 31, 13, 714, DateTimeKind.Local).AddTicks(2725), 3560, "Othello", 1 },
                    { 4, new DateTime(2024, 10, 1, 11, 31, 13, 714, DateTimeKind.Local).AddTicks(2726), 3227, "Henry V", 3 },
                    { 5, new DateTime(2024, 10, 1, 11, 31, 13, 714, DateTimeKind.Local).AddTicks(2727), 2648, "King John", 3 },
                    { 6, new DateTime(2024, 10, 1, 11, 31, 13, 714, DateTimeKind.Local).AddTicks(2727), 3718, "Richard III", 3 }
                });

            migrationBuilder.UpdateData(
                table: "TypeGenre",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 1, 11, 31, 13, 714, DateTimeKind.Local).AddTicks(3814));

            migrationBuilder.UpdateData(
                table: "TypeGenre",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 1, 11, 31, 13, 714, DateTimeKind.Local).AddTicks(3817));

            migrationBuilder.UpdateData(
                table: "TypeGenre",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 1, 11, 31, 13, 714, DateTimeKind.Local).AddTicks(3819));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Play",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Play",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Play",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Play",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Play",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Play",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "TypeGenre",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 1, 11, 31, 2, 457, DateTimeKind.Local).AddTicks(5402));

            migrationBuilder.UpdateData(
                table: "TypeGenre",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 1, 11, 31, 2, 457, DateTimeKind.Local).AddTicks(5411));

            migrationBuilder.UpdateData(
                table: "TypeGenre",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 1, 11, 31, 2, 457, DateTimeKind.Local).AddTicks(5413));
        }
    }
}
