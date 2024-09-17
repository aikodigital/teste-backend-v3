using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Statement_StatementId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "StatementId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Statement_StatementId",
                table: "Item",
                column: "StatementId",
                principalTable: "Statement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Statement_StatementId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "StatementId",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Statement_StatementId",
                table: "Item",
                column: "StatementId",
                principalTable: "Statement",
                principalColumn: "Id");
        }
    }
}
