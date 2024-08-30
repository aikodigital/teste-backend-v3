using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JogaresTeatrais.Data.Migrations
{
    public partial class AjusteMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Fatura",
                columns: new[] { "Id", "Cliente" },
                values: new object[] { 1, "Big Co" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fatura",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
