using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESTOQUE",
                columns: table => new
                {
                    IDESTOQUE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DATACRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTOQUE", x => x.IDESTOQUE);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    IDPRODUTO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PRECO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    DATACRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDESTOQUE = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.IDPRODUTO);
                    table.ForeignKey(
                        name: "FK_PRODUTO_ESTOQUE_IDESTOQUE",
                        column: x => x.IDESTOQUE,
                        principalTable: "ESTOQUE",
                        principalColumn: "IDESTOQUE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_IDESTOQUE",
                table: "PRODUTO",
                column: "IDESTOQUE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUTO");

            migrationBuilder.DropTable(
                name: "ESTOQUE");
        }
    }
}
