using Microsoft.EntityFrameworkCore.Migrations;

namespace BlackRiver.Data.Migrations
{
    public partial class UpdateReservas_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantidadePessoas",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadePessoas",
                table: "Reservas");
        }
    }
}
