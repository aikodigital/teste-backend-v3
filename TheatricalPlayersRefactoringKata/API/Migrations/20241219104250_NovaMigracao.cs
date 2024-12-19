using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class NovaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    PlayId = table.Column<string>(type: "TEXT", nullable: false),
                    Audience = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceRecordEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.PlayId);
                    table.ForeignKey(
                        name: "FK_Performance_Invoices_InvoiceRecordEntityId",
                        column: x => x.InvoiceRecordEntityId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Performance_InvoiceRecordEntityId",
                table: "Performance",
                column: "InvoiceRecordEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Performance");
        }
    }
}
