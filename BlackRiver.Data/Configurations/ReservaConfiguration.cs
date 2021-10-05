using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(p => p.ValorDiaria)
                .HasPrecision(7,2);

            builder.Property(p => p.Status)
                .HasMaxLength(50);

            builder.HasMany<Hospede>();
        }
    }

}
