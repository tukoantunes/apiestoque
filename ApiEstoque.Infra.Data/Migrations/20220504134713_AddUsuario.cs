using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Infra.Data.Migrations
{
    public partial class AddUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IDUSUARIO",
                table: "PRODUTO",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IDUSUARIO",
                table: "ESTOQUE",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    IDUSUARIO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DATACRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.IDUSUARIO);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_IDUSUARIO",
                table: "PRODUTO",
                column: "IDUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_ESTOQUE_IDUSUARIO",
                table: "ESTOQUE",
                column: "IDUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_EMAIL",
                table: "USUARIO",
                column: "EMAIL",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ESTOQUE_USUARIO_IDUSUARIO",
                table: "ESTOQUE",
                column: "IDUSUARIO",
                principalTable: "USUARIO",
                principalColumn: "IDUSUARIO");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTO_USUARIO_IDUSUARIO",
                table: "PRODUTO",
                column: "IDUSUARIO",
                principalTable: "USUARIO",
                principalColumn: "IDUSUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ESTOQUE_USUARIO_IDUSUARIO",
                table: "ESTOQUE");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTO_USUARIO_IDUSUARIO",
                table: "PRODUTO");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropIndex(
                name: "IX_PRODUTO_IDUSUARIO",
                table: "PRODUTO");

            migrationBuilder.DropIndex(
                name: "IX_ESTOQUE_IDUSUARIO",
                table: "ESTOQUE");

            migrationBuilder.DropColumn(
                name: "IDUSUARIO",
                table: "PRODUTO");

            migrationBuilder.DropColumn(
                name: "IDUSUARIO",
                table: "ESTOQUE");
        }
    }
}
