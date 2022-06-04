using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Repository.Migrations
{
    public partial class updatecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Credits",
                table: "Invoice",
                newName: "TotalCredits");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Invoice",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountOwned",
                table: "Performance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EarnedCredits",
                table: "Performance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOwned",
                table: "Performance");

            migrationBuilder.DropColumn(
                name: "EarnedCredits",
                table: "Performance");

            migrationBuilder.RenameColumn(
                name: "TotalCredits",
                table: "Invoice",
                newName: "Credits");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Invoice",
                newName: "Amount");
        }
    }
}
