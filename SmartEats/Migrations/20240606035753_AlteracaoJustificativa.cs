using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEats.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoJustificativa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aprovado",
                table: "Justifies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdConfirmacao",
                table: "Justifies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MotivoRecusa",
                table: "Justifies",
                type: "longtext",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdFuncionario",
                table: "Confirms",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");


            migrationBuilder.CreateIndex(
                name: "IX_Justifies_IdConfirmacao",
                table: "Justifies",
                column: "IdConfirmacao");

            migrationBuilder.CreateIndex(
                name: "IX_Confirms_IdFuncionario",
                table: "Confirms",
                column: "IdFuncionario");

            migrationBuilder.AddForeignKey(
                name: "FK_Confirms_AspNetUsers_IdFuncionario",
                table: "Confirms",
                column: "IdFuncionario",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Justifies_Confirms_IdConfirmacao",
                table: "Justifies",
                column: "IdConfirmacao",
                principalTable: "Confirms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confirms_AspNetUsers_IdFuncionario",
                table: "Confirms");

            migrationBuilder.DropForeignKey(
                name: "FK_Justifies_Confirms_IdConfirmacao",
                table: "Justifies");

            migrationBuilder.DropIndex(
                name: "IX_Justifies_IdConfirmacao",
                table: "Justifies");

            migrationBuilder.DropIndex(
                name: "IX_Confirms_IdFuncionario",
                table: "Confirms");

            migrationBuilder.DropColumn(
                name: "Aprovado",
                table: "Justifies");

            migrationBuilder.DropColumn(
                name: "IdConfirmacao",
                table: "Justifies");

            migrationBuilder.DropColumn(
                name: "MotivoRecusa",
                table: "Justifies");

            migrationBuilder.AlterColumn<string>(
                name: "IdFuncionario",
                table: "Confirms",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
