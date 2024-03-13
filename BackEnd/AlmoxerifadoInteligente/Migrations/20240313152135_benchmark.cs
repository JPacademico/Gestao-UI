using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmoxerifadoInteligente.Migrations
{
    public partial class benchmark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenchmarkingItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeLoja1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeLoja2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoLoja1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoLoja2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Economia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    IdProdutoNavigationIdProduto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenchmarkingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenchmarkingItem_Produtos_IdProdutoNavigationIdProduto",
                        column: x => x.IdProdutoNavigationIdProduto,
                        principalTable: "Produtos",
                        principalColumn: "id_produto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkingItem_IdProdutoNavigationIdProduto",
                table: "BenchmarkingItem",
                column: "IdProdutoNavigationIdProduto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenchmarkingItem");
        }
    }
}
