using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEats.Migrations
{
    /// <inheritdoc />
    public partial class NovasTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Companies",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CNPJ",
                table: "Companies",
                column: "CNPJ",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companies_CNPJ",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Companies");
        }
    }
}
