using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data.Configurations
{
    public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .HasMaxLength(50);

            builder.Property(f => f.RG)
                .HasMaxLength(12);

            builder.Property(f => f.CPF)
                .HasMaxLength(14);

            builder.Property(f => f.CTPS)
                .HasMaxLength(20);

            builder.Property(f => f.Telefone)
                .HasMaxLength(14);

            builder.Property(f => f.Endereco)
                .HasMaxLength(100);

            builder.Property(f => f.Email)
                .HasMaxLength(70);

            builder.Property(f => f.Departamento)
                .HasMaxLength(30);

            builder.Property(f => f.Cargo)
                .HasMaxLength(50);
        }
    }

}
