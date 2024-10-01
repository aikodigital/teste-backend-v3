using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheatricalPlayersRefactoringKata.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerStatement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Customer = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TotalAmount = table.Column<int>(type: "integer", nullable: false),
                    VolumeCredits = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerStatement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Audience = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BasePriceMultiplier = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    MaxAudience = table.Column<int>(type: "integer", nullable: true),
                    ExtraFeePerAudience = table.Column<int>(type: "integer", nullable: true),
                    BaseFeePerAudience = table.Column<int>(type: "integer", nullable: true),
                    BonusFee = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeGenre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerStatementProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    CustomerStatementId = table.Column<int>(type: "integer", nullable: false),
                    Process = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerStatementProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerStatementProcess_CustomerStatement_CustomerStatemen~",
                        column: x => x.CustomerStatementId,
                        principalTable: "CustomerStatement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Play",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Lines = table.Column<int>(type: "integer", nullable: false),
                    TypeGenreId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Play", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Play_TypeGenre_TypeGenreId",
                        column: x => x.TypeGenreId,
                        principalTable: "TypeGenre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPlaysStatement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    CustomerStatementId = table.Column<int>(type: "integer", nullable: false),
                    PlayId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    TotalSeats = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPlaysStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPlaysStatement_CustomerStatement_CustomerStatementId",
                        column: x => x.CustomerStatementId,
                        principalTable: "CustomerStatement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPlaysStatement_Play_PlayId",
                        column: x => x.PlayId,
                        principalTable: "Play",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Audience = table.Column<int>(type: "integer", nullable: false),
                    PlayId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performance_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Performance_Play_PlayId",
                        column: x => x.PlayId,
                        principalTable: "Play",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TypeGenre",
                columns: new[] { "Id", "BaseFeePerAudience", "BasePriceMultiplier", "BonusFee", "CreatedAt", "ExtraFeePerAudience", "MaxAudience", "Name" },
                values: new object[,]
                {
                    { 1, null, 10, null, new DateTime(2024, 10, 1, 11, 31, 2, 457, DateTimeKind.Local).AddTicks(5402), 1000, 30, "tragedy" },
                    { 2, 300, 10, 10000, new DateTime(2024, 10, 1, 11, 31, 2, 457, DateTimeKind.Local).AddTicks(5411), 500, 20, "comedy" },
                    { 3, null, 10, null, new DateTime(2024, 10, 1, 11, 31, 2, 457, DateTimeKind.Local).AddTicks(5413), null, null, "history" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPlaysStatement_CustomerStatementId",
                table: "CustomerPlaysStatement",
                column: "CustomerStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPlaysStatement_PlayId",
                table: "CustomerPlaysStatement",
                column: "PlayId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStatementProcess_CustomerStatementId",
                table: "CustomerStatementProcess",
                column: "CustomerStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_InvoiceId",
                table: "Performance",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_PlayId",
                table: "Performance",
                column: "PlayId");

            migrationBuilder.CreateIndex(
                name: "IX_Play_TypeGenreId",
                table: "Play",
                column: "TypeGenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerPlaysStatement");

            migrationBuilder.DropTable(
                name: "CustomerStatementProcess");

            migrationBuilder.DropTable(
                name: "Performance");

            migrationBuilder.DropTable(
                name: "CustomerStatement");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Play");

            migrationBuilder.DropTable(
                name: "TypeGenre");
        }
    }
}
