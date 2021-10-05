using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace BlackRiver.Data
{
    public class BlackRiverDBContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        public DbSet<VagaEstacionamento> VagasEstacionamento { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<Hospede> Hospedes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Quarto> Quartos { get; set; }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder
                .UseSqlServer(BlackRiverConstants.ConnectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetAssembly(typeof(BlackRiverDBContext))
                 .GetTypes()
                 .Where(type => type.Namespace != null && type.Namespace.Equals(typeof(BlackRiverDBContext)))
                 .Where(type => type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic instance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(instance);
            }
        }
    }
}
