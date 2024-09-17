using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Plays_PlayId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PlayId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PlayId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "LoyaltyCredit",
                table: "Invoices",
                newName: "Seats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Seats",
                table: "Invoices",
                newName: "LoyaltyCredit");

            migrationBuilder.AddColumn<long>(
                name: "PlayId",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PlayId",
                table: "Invoices",
                column: "PlayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Plays_PlayId",
                table: "Invoices",
                column: "PlayId",
                principalTable: "Plays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
