using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TheatricalPlayersAPI.Migrations
{
    public partial class _002createtablestatements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                    name: "Statements",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                        Statement = table.Column<string>(type: "longtext", nullable: true),
                        StatementXml = table.Column<string>(type: "longtext", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Statements", x => x.Id);
                    })
                .Annotation("MySQL:Charset", "utf8mb4");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatementXml");
        }
    }
}
