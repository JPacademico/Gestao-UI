using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmoxerifadoInteligente.Migrations
{
    public partial class _1303 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "BenchmarkingItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "LinkLoja1",
                table: "BenchmarkingItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkLoja2",
                table: "BenchmarkingItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkLoja1",
                table: "BenchmarkingItem");

            migrationBuilder.DropColumn(
                name: "LinkLoja2",
                table: "BenchmarkingItem");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "BenchmarkingItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
