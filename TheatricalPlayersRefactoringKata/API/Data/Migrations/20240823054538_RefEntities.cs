using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatricalPlayersRefactoringKata.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceInvoices_Invoices_InvoicesId",
                table: "PerformanceInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceInvoices_Performances_PerformancesId",
                table: "PerformanceInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerformanceInvoices",
                table: "PerformanceInvoices");

            migrationBuilder.RenameTable(
                name: "PerformanceInvoices",
                newName: "InvoicePerformance");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceInvoices_PerformancesId",
                table: "InvoicePerformance",
                newName: "IX_InvoicePerformance_PerformancesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plays",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Customer",
                table: "Invoices",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoicePerformance",
                table: "InvoicePerformance",
                columns: new[] { "InvoicesId", "PerformancesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePerformance_Invoices_InvoicesId",
                table: "InvoicePerformance",
                column: "InvoicesId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePerformance_Performances_PerformancesId",
                table: "InvoicePerformance",
                column: "PerformancesId",
                principalTable: "Performances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePerformance_Invoices_InvoicesId",
                table: "InvoicePerformance");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePerformance_Performances_PerformancesId",
                table: "InvoicePerformance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoicePerformance",
                table: "InvoicePerformance");

            migrationBuilder.RenameTable(
                name: "InvoicePerformance",
                newName: "PerformanceInvoices");

            migrationBuilder.RenameIndex(
                name: "IX_InvoicePerformance_PerformancesId",
                table: "PerformanceInvoices",
                newName: "IX_PerformanceInvoices_PerformancesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plays",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Customer",
                table: "Invoices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerformanceInvoices",
                table: "PerformanceInvoices",
                columns: new[] { "InvoicesId", "PerformancesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceInvoices_Invoices_InvoicesId",
                table: "PerformanceInvoices",
                column: "InvoicesId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceInvoices_Performances_PerformancesId",
                table: "PerformanceInvoices",
                column: "PerformancesId",
                principalTable: "Performances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
