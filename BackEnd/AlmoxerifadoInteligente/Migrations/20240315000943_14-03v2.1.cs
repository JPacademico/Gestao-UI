using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmoxerifadoInteligente.Migrations
{
    public partial class _1403v21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "BenchmarkingItem");

            migrationBuilder.AddColumn<bool>(
                name: "EmailStatus",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailStatus",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BenchmarkingItem",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
