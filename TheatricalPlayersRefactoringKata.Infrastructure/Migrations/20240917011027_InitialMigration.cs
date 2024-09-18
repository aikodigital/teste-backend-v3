using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceCalculeteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimumAudience = table.Column<int>(type: "INTEGER", nullable: false),
                    Bonus = table.Column<decimal>(type: "TEXT", nullable: false),
                    PerAudienceAdditional = table.Column<decimal>(type: "TEXT", nullable: false),
                    PerAudience = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCalculeteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceCreditSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimumAudience = table.Column<int>(type: "INTEGER", nullable: false),
                    BonusCreditPerAttendees = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCreditSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Customer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    PerformanceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayId = table.Column<int>(type: "INTEGER", nullable: false),
                    Audience = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.PerformanceId);
                });

            migrationBuilder.CreateTable(
                name: "Play",
                columns: table => new
                {
                    PlayId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Lines = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Play", x => x.PlayId);
                });

            migrationBuilder.CreateTable(
                name: "PlayType",
                columns: table => new
                {
                    PlayTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayType", x => x.PlayTypeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceCalculeteSettings");

            migrationBuilder.DropTable(
                name: "InvoiceCreditSettings");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Play");

            migrationBuilder.DropTable(
                name: "PlayType");
        }
    }
}
