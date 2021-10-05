using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasMaxLength(50);

            builder.Property(p => p.Marca)
                .HasMaxLength(50);

            builder.Property(p => p.Lote)
                .HasMaxLength(5);

            builder.Property(p => p.Tipo)
                .HasMaxLength(15);

            builder.Property(p => p.Fornecedor)
                .HasMaxLength(100);

            builder.Property(p => p.Valor)
                .HasPrecision(7, 2);
        }
    }
}
