using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Infra.Migrations
{
    public partial class _2ndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Performance_Invoice_InvoiceId",
                table: "Performance");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Performance",
                newName: "InvoiceEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Performance_InvoiceId",
                table: "Performance",
                newName: "IX_Performance_InvoiceEntityId");

            migrationBuilder.AddColumn<int>(
                name: "PlayId",
                table: "Performance",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Performance_PlayId",
                table: "Performance",
                column: "PlayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Performance_Invoice_InvoiceEntityId",
                table: "Performance",
                column: "InvoiceEntityId",
                principalTable: "Invoice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Performance_Play_PlayId",
                table: "Performance",
                column: "PlayId",
                principalTable: "Play",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Performance_Invoice_InvoiceEntityId",
                table: "Performance");

            migrationBuilder.DropForeignKey(
                name: "FK_Performance_Play_PlayId",
                table: "Performance");

            migrationBuilder.DropIndex(
                name: "IX_Performance_PlayId",
                table: "Performance");

            migrationBuilder.DropColumn(
                name: "PlayId",
                table: "Performance");

            migrationBuilder.RenameColumn(
                name: "InvoiceEntityId",
                table: "Performance",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Performance_InvoiceEntityId",
                table: "Performance",
                newName: "IX_Performance_InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Performance_Invoice_InvoiceId",
                table: "Performance",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");
        }
    }
}
