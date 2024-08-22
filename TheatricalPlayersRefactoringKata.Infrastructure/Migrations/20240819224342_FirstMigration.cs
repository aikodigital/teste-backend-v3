using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    performance_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_plays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lines = table.Column<int>(type: "integer", nullable: false),
                    genre = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_plays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_performances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayId = table.Column<Guid>(type: "uuid", nullable: false),
                    audience = table.Column<int>(type: "integer", nullable: false),
                    credits = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_performances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_performances_tb_plays_PlayId",
                        column: x => x.PlayId,
                        principalTable: "tb_plays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_invoice_performance",
                columns: table => new
                {
                    InvoicesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PerformancesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_invoice_performance", x => new { x.InvoicesId, x.PerformancesId });
                    table.ForeignKey(
                        name: "FK_tb_invoice_performance_tb_invoices_InvoicesId",
                        column: x => x.InvoicesId,
                        principalTable: "tb_invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_invoice_performance_tb_performances_PerformancesId",
                        column: x => x.PerformancesId,
                        principalTable: "tb_performances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_invoice_performance_PerformancesId",
                table: "tb_invoice_performance",
                column: "PerformancesId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_performances_PlayId",
                table: "tb_performances",
                column: "PlayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_invoice_performance");

            migrationBuilder.DropTable(
                name: "tb_invoices");

            migrationBuilder.DropTable(
                name: "tb_performances");

            migrationBuilder.DropTable(
                name: "tb_plays");
        }
    }
}
