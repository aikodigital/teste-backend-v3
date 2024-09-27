using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheatricalPlayersRefactoringKata.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TypeGenre",
                columns: new[] { "Id", "BaseFeePerAudience", "BasePriceMultiplier", "BonusFee", "CreatedAt", "ExtraFeePerAudience", "MaxAudience", "Name" },
                values: new object[,]
                {
                    { 1, null, 10, null, new DateTime(2024, 9, 26, 13, 35, 46, 775, DateTimeKind.Local).AddTicks(6048), 1000, 30, "tragedy" },
                    { 2, 300, 10, 10000, new DateTime(2024, 9, 26, 13, 35, 46, 775, DateTimeKind.Local).AddTicks(6064), 500, 20, "comedy" },
                    { 3, null, 10, null, new DateTime(2024, 9, 26, 13, 35, 46, 775, DateTimeKind.Local).AddTicks(6066), null, null, "history" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeGenre",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypeGenre",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TypeGenre",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
