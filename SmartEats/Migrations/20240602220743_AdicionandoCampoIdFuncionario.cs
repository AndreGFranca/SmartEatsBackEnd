using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEats.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCampoIdFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "IdFuncionario",
                table: "Confirms",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFuncionario",
                table: "Confirms");
        }
    }
}
