using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Migrations
{
    /// <inheritdoc />
    public partial class CreatePlayName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnedCredits",
                table: "Statements",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "EarnedCredits",
                table: "StatementItems",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StatementItems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "StatementItems");

            migrationBuilder.AlterColumn<int>(
                name: "TotalEarnedCredits",
                table: "Statements",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "EarnedCredits",
                table: "StatementItems",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
