using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data
{
    public class QuartoConfiguration : IEntityTypeConfiguration<Quarto>
    {
        public void Configure(EntityTypeBuilder<Quarto> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.TipoQuarto)
                .HasMaxLength(15);

            builder.Property(q => q.ValorQuarto)
                .HasPrecision(7, 2);
        }
    }
}
