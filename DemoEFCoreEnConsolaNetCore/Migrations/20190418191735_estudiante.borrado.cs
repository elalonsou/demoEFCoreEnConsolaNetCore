using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoEFCoreEnConsolaNetCore.Migrations
{
    public partial class estudianteborrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Borrado",
                table: "Estudiantes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Borrado",
                table: "Estudiantes");
        }
    }
}
