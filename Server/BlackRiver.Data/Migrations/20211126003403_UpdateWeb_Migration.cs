using Microsoft.EntityFrameworkCore.Migrations;

namespace BlackRiver.Data.Migrations
{
    public partial class UpdateWeb_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Vip",
                table: "Quartos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "Ocorrencias",
                type: "real(2)",
                precision: 2,
                scale: 1,
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vip",
                table: "Quartos");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Ocorrencias");
        }
    }
}
