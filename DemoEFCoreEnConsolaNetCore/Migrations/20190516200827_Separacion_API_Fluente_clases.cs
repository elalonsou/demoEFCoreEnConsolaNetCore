using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoEFCoreEnConsolaNetCore.Migrations
{
    public partial class Separacion_API_Fluente_clases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Curso",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curso_Nombre",
                table: "Curso",
                column: "Nombre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Curso_Nombre",
                table: "Curso");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Curso",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
