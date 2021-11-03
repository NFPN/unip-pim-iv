using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlackRiver.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hoteis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MunicipioAtualId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hoteis_Municipios_MunicipioAtualId",
                        column: x => x.MunicipioAtualId,
                        principalTable: "Municipios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    RG = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    CTPS = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Departamento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HotelAtualId = table.Column<int>(type: "int", nullable: true),
                    MunicipioAtualId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Hoteis_HotelAtualId",
                        column: x => x.HotelAtualId,
                        principalTable: "Hoteis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Municipios_MunicipioAtualId",
                        column: x => x.MunicipioAtualId,
                        principalTable: "Municipios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quartos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoQuarto = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    NumeroQuarto = table.Column<int>(type: "int", nullable: false),
                    NumeroAndar = table.Column<int>(type: "int", nullable: false),
                    ValorQuarto = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    StatusQuarto = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quartos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quartos_Hoteis_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hoteis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Fornecedor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    QuantidadeDisponivel = table.Column<int>(type: "int", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAquisicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: true),
                    VendaId = table.Column<int>(type: "int", nullable: true),
                    VendaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HospedeReserva",
                columns: table => new
                {
                    HospedesId = table.Column<int>(type: "int", nullable: false),
                    ReservasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospedeReserva", x => new { x.HospedesId, x.ReservasId });
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ReservaId = table.Column<int>(type: "int", nullable: true),
                    HospedeId = table.Column<int>(type: "int", nullable: true),
                    HospedeId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuartoId = table.Column<int>(type: "int", nullable: true),
                    ValorDiaria = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCancelamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HospedeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Quartos_QuartoId",
                        column: x => x.QuartoId,
                        principalTable: "Quartos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hospedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    RG = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    ReservaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospedes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospedes_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VagasEstacionamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroVaga = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Placa = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    HospedeId = table.Column<int>(type: "int", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagasEstacionamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VagasEstacionamento_Hospedes_HospedeId",
                        column: x => x.HospedeId,
                        principalTable: "Hospedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VagasEstacionamento_Hoteis_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hoteis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HospedePagadorId = table.Column<int>(type: "int", nullable: true),
                    ProdutoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendas_Hospedes_HospedePagadorId",
                        column: x => x.HospedePagadorId,
                        principalTable: "Hospedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_HotelAtualId",
                table: "Funcionarios",
                column: "HotelAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_MunicipioAtualId",
                table: "Funcionarios",
                column: "MunicipioAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_HospedeReserva_ReservasId",
                table: "HospedeReserva",
                column: "ReservasId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospedes_ReservaId",
                table: "Hospedes",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteis_MunicipioAtualId",
                table: "Hoteis",
                column: "MunicipioAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_HospedeId",
                table: "Ocorrencias",
                column: "HospedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_HospedeId1",
                table: "Ocorrencias",
                column: "HospedeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_ReservaId",
                table: "Ocorrencias",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_VendaId",
                table: "Produtos",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_VendaId1",
                table: "Produtos",
                column: "VendaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Quartos_HotelId",
                table: "Quartos",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_HospedeId",
                table: "Reservas",
                column: "HospedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_QuartoId",
                table: "Reservas",
                column: "QuartoId");

            migrationBuilder.CreateIndex(
                name: "IX_VagasEstacionamento_HospedeId",
                table: "VagasEstacionamento",
                column: "HospedeId");

            migrationBuilder.CreateIndex(
                name: "IX_VagasEstacionamento_HotelId",
                table: "VagasEstacionamento",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_HospedePagadorId",
                table: "Vendas",
                column: "HospedePagadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ProdutoId",
                table: "Vendas",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Vendas_VendaId",
                table: "Produtos",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Vendas_VendaId1",
                table: "Produtos",
                column: "VendaId1",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HospedeReserva_Hospedes_HospedesId",
                table: "HospedeReserva",
                column: "HospedesId",
                principalTable: "Hospedes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HospedeReserva_Reservas_ReservasId",
                table: "HospedeReserva",
                column: "ReservasId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencias_Hospedes_HospedeId",
                table: "Ocorrencias",
                column: "HospedeId",
                principalTable: "Hospedes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencias_Hospedes_HospedeId1",
                table: "Ocorrencias",
                column: "HospedeId1",
                principalTable: "Hospedes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencias_Reservas_ReservaId",
                table: "Ocorrencias",
                column: "ReservaId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Hospedes_HospedeId",
                table: "Reservas",
                column: "HospedeId",
                principalTable: "Hospedes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quartos_Hoteis_HotelId",
                table: "Quartos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Hospedes_HospedeId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Hospedes_HospedePagadorId",
                table: "Vendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categoria_CategoriaId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Vendas_VendaId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Vendas_VendaId1",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "HospedeReserva");

            migrationBuilder.DropTable(
                name: "Ocorrencias");

            migrationBuilder.DropTable(
                name: "VagasEstacionamento");

            migrationBuilder.DropTable(
                name: "Hoteis");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Hospedes");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Quartos");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
