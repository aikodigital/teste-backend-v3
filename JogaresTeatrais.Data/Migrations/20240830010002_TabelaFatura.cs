using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JogaresTeatrais.Data.Migrations
{
    public partial class TabelaFatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FaturaId",
                table: "Desempenho",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fatura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cliente = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatura", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desempenho_FaturaId",
                table: "Desempenho",
                column: "FaturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Desempenho_Fatura_FaturaId",
                table: "Desempenho",
                column: "FaturaId",
                principalTable: "Fatura",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desempenho_Fatura_FaturaId",
                table: "Desempenho");

            migrationBuilder.DropTable(
                name: "Fatura");

            migrationBuilder.DropIndex(
                name: "IX_Desempenho_FaturaId",
                table: "Desempenho");

            migrationBuilder.DropColumn(
                name: "FaturaId",
                table: "Desempenho");
        }
    }
}
