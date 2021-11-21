using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace BlackRiver.Data
{
    public class BlackRiverDBContext : DbContext
    {
        public DbSet<Hotel> Hoteis { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        public DbSet<VagaEstacionamento> VagasEstacionamento { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<Hospede> Hospedes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Quarto> Quartos { get; set; }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoCategoria> Categorias { get; set; }

        public DbSet<Login> Users { get; set; }


        public BlackRiverDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
