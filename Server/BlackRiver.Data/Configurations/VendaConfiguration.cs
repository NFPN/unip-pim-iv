using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data
{
    public class VendaConfiguration : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                .UseIdentityColumn();

            builder.Property(v => v.ValorPago)
                .HasPrecision(7, 2);

            builder.HasMany(v => v.Produtos)
                .WithMany(p => p.Vendas);
        }
    }
}
