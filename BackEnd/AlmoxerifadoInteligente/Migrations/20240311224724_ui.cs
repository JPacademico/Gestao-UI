using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmoxerifadoInteligente.Migrations
{
    public partial class ui : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    id_log = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoRobo = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    UsuarioRobo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Etapa = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    InformacaoLog = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    id_produto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Logs__6CC851FEC09C9E3D", x => x.id_log);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    id_produto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    preco = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    estoque_atual = table.Column<int>(type: "int", nullable: false),
                    estoque_minimo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Produtos__BA38A6B801B4C2DA", x => x.id_produto);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
