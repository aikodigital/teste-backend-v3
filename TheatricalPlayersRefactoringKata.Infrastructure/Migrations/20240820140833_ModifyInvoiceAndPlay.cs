using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyInvoiceAndPlay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "amount",
                table: "tb_plays",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "total_amount",
                table: "tb_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "total_credits",
                table: "tb_invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "tb_plays");

            migrationBuilder.DropColumn(
                name: "total_amount",
                table: "tb_invoices");

            migrationBuilder.DropColumn(
                name: "total_credits",
                table: "tb_invoices");
        }
    }
}
