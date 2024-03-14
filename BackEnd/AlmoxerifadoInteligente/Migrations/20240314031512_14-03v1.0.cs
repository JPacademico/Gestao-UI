using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmoxerifadoInteligente.Migrations
{
    public partial class _1403v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Produtos",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Produtos");
        }
    }
}
