﻿// <auto-generated />
using System;
using BlackRiver.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlackRiver.Data.Migrations
{
    [DbContext(typeof(BlackRiverDBContext))]
    [Migration("20211126003403_updateweb")]
    partial class UpdateWeb_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlackRiver.EntityModels.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("CTPS")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Cargo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Departamento")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Email")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Endereco")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("HotelAtualId")
                        .HasColumnType("int");

                    b.Property<int?>("LoginId")
                        .HasColumnType("int");

                    b.Property<int?>("MunicipioAtualId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RG")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasKey("Id");

                    b.HasIndex("HotelAtualId");

                    b.HasIndex("LoginId");

                    b.HasIndex("MunicipioAtualId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Hospede", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CEP")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("CPF")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Endereco")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int?>("LoginId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RG")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int?>("VagaEstacionamentoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LoginId");

                    b.HasIndex("VagaEstacionamentoId");

                    b.ToTable("Hospedes");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Endereco")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("MunicipioAtualId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioAtualId");

                    b.ToTable("Hoteis");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UF")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Municipios");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Ocorrencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Departamento")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Descricao")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("HospedeId")
                        .HasColumnType("int");

                    b.Property<int?>("ReservaId")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasPrecision(2, 1)
                        .HasColumnType("real(2)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("HospedeId");

                    b.HasIndex("ReservaId");

                    b.ToTable("Ocorrencias");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAquisicao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fornecedor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Lote")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Marca")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nome")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("QuantidadeDisponivel")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("Valor")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.ProdutoCategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Quarto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("NumeroAndar")
                        .HasColumnType("int");

                    b.Property<int>("NumeroQuarto")
                        .HasColumnType("int");

                    b.Property<int>("StatusQuarto")
                        .HasColumnType("int");

                    b.Property<int>("TipoQuarto")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.Property<decimal>("ValorQuarto")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<bool>("Vip")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Quartos");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCancelamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataSaida")
                        .HasColumnType("datetime2");

                    b.Property<int?>("QuartoId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("ValorDiaria")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.HasKey("Id");

                    b.HasIndex("QuartoId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.UserLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.VagaEstacionamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("NumeroVaga")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Placa")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("VagasEstacionamento");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HospedePagadorId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorPago")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.HasKey("Id");

                    b.HasIndex("HospedePagadorId");

                    b.ToTable("Vendas");
                });

            modelBuilder.Entity("HospedeReserva", b =>
                {
                    b.Property<int>("HospedesId")
                        .HasColumnType("int");

                    b.Property<int>("ReservasId")
                        .HasColumnType("int");

                    b.HasKey("HospedesId", "ReservasId");

                    b.HasIndex("ReservasId");

                    b.ToTable("HospedeReserva");
                });

            modelBuilder.Entity("ProdutoVenda", b =>
                {
                    b.Property<int>("ProdutosId")
                        .HasColumnType("int");

                    b.Property<int>("VendasId")
                        .HasColumnType("int");

                    b.HasKey("ProdutosId", "VendasId");

                    b.HasIndex("VendasId");

                    b.ToTable("ProdutoVenda");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Funcionario", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.Hotel", "HotelAtual")
                        .WithMany()
                        .HasForeignKey("HotelAtualId");

                    b.HasOne("BlackRiver.EntityModels.UserLogin", "Login")
                        .WithMany()
                        .HasForeignKey("LoginId");

                    b.HasOne("BlackRiver.EntityModels.Municipio", "MunicipioAtual")
                        .WithMany()
                        .HasForeignKey("MunicipioAtualId");

                    b.Navigation("HotelAtual");

                    b.Navigation("Login");

                    b.Navigation("MunicipioAtual");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Hospede", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.UserLogin", "Login")
                        .WithMany()
                        .HasForeignKey("LoginId");

                    b.HasOne("BlackRiver.EntityModels.VagaEstacionamento", "VagaEstacionamento")
                        .WithMany()
                        .HasForeignKey("VagaEstacionamentoId");

                    b.Navigation("Login");

                    b.Navigation("VagaEstacionamento");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Hotel", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.Municipio", "MunicipioAtual")
                        .WithMany()
                        .HasForeignKey("MunicipioAtualId");

                    b.Navigation("MunicipioAtual");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Ocorrencia", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.Hospede", null)
                        .WithMany("Ocorrencias")
                        .HasForeignKey("HospedeId");

                    b.HasOne("BlackRiver.EntityModels.Reserva", "Reserva")
                        .WithMany()
                        .HasForeignKey("ReservaId");

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Produto", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.ProdutoCategoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Quarto", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.Hotel", null)
                        .WithMany("Quartos")
                        .HasForeignKey("HotelId");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Reserva", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.Quarto", "Quarto")
                        .WithMany()
                        .HasForeignKey("QuartoId");

                    b.Navigation("Quarto");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.VagaEstacionamento", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.Hotel", null)
                        .WithMany("VagasEstacionamento")
                        .HasForeignKey("HotelId");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Venda", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.Hospede", "HospedePagador")
                        .WithMany()
                        .HasForeignKey("HospedePagadorId");

                    b.Navigation("HospedePagador");
                });

            modelBuilder.Entity("HospedeReserva", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.Hospede", null)
                        .WithMany()
                        .HasForeignKey("HospedesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlackRiver.EntityModels.Reserva", null)
                        .WithMany()
                        .HasForeignKey("ReservasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProdutoVenda", b =>
                {
                    b.HasOne("BlackRiver.EntityModels.Produto", null)
                        .WithMany()
                        .HasForeignKey("ProdutosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlackRiver.EntityModels.Venda", null)
                        .WithMany()
                        .HasForeignKey("VendasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Hospede", b =>
                {
                    b.Navigation("Ocorrencias");
                });

            modelBuilder.Entity("BlackRiver.EntityModels.Hotel", b =>
                {
                    b.Navigation("Quartos");

                    b.Navigation("VagasEstacionamento");
                });
#pragma warning restore 612, 618
        }
    }
}
