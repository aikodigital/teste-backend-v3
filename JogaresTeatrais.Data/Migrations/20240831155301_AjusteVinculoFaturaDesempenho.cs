using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JogaresTeatrais.Data.Migrations
{
    public partial class AjusteVinculoFaturaDesempenho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Desempenho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JogarId = table.Column<int>(type: "INTEGER", nullable: false),
                    Audiencia = table.Column<int>(type: "INTEGER", nullable: false),
                    FaturaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desempenho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Desempenho_Fatura_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Fatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Fatura",
                columns: new[] { "Id", "Cliente" },
                values: new object[] { 1, "Big Co" });

            migrationBuilder.InsertData(
                table: "Jogar",
                columns: new[] { "Id", "Linhas", "Nome", "Tipo" },
                values: new object[] { 1, 4024, "Hamlet", "tragedy" });

            migrationBuilder.InsertData(
                table: "Jogar",
                columns: new[] { "Id", "Linhas", "Nome", "Tipo" },
                values: new object[] { 2, 2670, "As You Like It", "comedy" });

            migrationBuilder.InsertData(
                table: "Jogar",
                columns: new[] { "Id", "Linhas", "Nome", "Tipo" },
                values: new object[] { 3, 3560, "Othello", "tragedy" });

            migrationBuilder.InsertData(
                table: "Jogar",
                columns: new[] { "Id", "Linhas", "Nome", "Tipo" },
                values: new object[] { 4, 3227, "Henry V", "history" });

            migrationBuilder.InsertData(
                table: "Jogar",
                columns: new[] { "Id", "Linhas", "Nome", "Tipo" },
                values: new object[] { 5, 3560, "John", "history" });

            migrationBuilder.InsertData(
                table: "Jogar",
                columns: new[] { "Id", "Linhas", "Nome", "Tipo" },
                values: new object[] { 6, 3718, "Richard-III", "history" });

            migrationBuilder.InsertData(
                table: "Desempenho",
                columns: new[] { "Id", "Audiencia", "FaturaId", "JogarId" },
                values: new object[] { 1, 55, 1, 1 });

            migrationBuilder.InsertData(
                table: "Desempenho",
                columns: new[] { "Id", "Audiencia", "FaturaId", "JogarId" },
                values: new object[] { 3, 35, 1, 2 });

            migrationBuilder.InsertData(
                table: "Desempenho",
                columns: new[] { "Id", "Audiencia", "FaturaId", "JogarId" },
                values: new object[] { 4, 40, 1, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Desempenho_FaturaId",
                table: "Desempenho",
                column: "FaturaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desempenho");

            migrationBuilder.DropTable(
                name: "Fatura");

            migrationBuilder.DeleteData(
                table: "Jogar",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jogar",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jogar",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Jogar",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Jogar",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Jogar",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
