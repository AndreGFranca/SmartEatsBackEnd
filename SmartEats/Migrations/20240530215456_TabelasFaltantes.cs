using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace SmartEats.Migrations
{
    /// <inheritdoc />
    public partial class TabelasFaltantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Confirms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DataConfirmacao = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraDeAlmoco = table.Column<TimeOnly>(type: "time", nullable: false),
                    Compareceu = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HorarioComparecimento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Confirmou = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confirms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Confirms_Companies_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Justifies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Justificativa = table.Column<string>(type: "longtext", nullable: false),
                    IdFuncionario = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdAprovador = table.Column<string>(type: "varchar(255)", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Justifies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Justifies_AspNetUsers_IdAprovador",
                        column: x => x.IdAprovador,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Justifies_AspNetUsers_IdFuncionario",
                        column: x => x.IdFuncionario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Justifies_Companies_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => new { x.Data, x.IdEmpresa });
                    table.ForeignKey(
                        name: "FK_Menus_Companies_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlatesDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Prato = table.Column<string>(type: "longtext", nullable: false),
                    CardapioDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatesDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatesDay_Menus_CardapioDate_CompanyId",
                        columns: x => new { x.CardapioDate, x.CompanyId },
                        principalTable: "Menus",
                        principalColumns: new[] { "Data", "IdEmpresa" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Confirms_IdEmpresa",
                table: "Confirms",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Justifies_IdAprovador",
                table: "Justifies",
                column: "IdAprovador");

            migrationBuilder.CreateIndex(
                name: "IX_Justifies_IdEmpresa",
                table: "Justifies",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Justifies_IdFuncionario",
                table: "Justifies",
                column: "IdFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_IdEmpresa",
                table: "Menus",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_PlatesDay_CardapioDate_CompanyId",
                table: "PlatesDay",
                columns: new[] { "CardapioDate", "CompanyId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Confirms");

            migrationBuilder.DropTable(
                name: "Justifies");

            migrationBuilder.DropTable(
                name: "PlatesDay");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
