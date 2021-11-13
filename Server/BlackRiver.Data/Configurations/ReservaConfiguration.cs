using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .UseIdentityColumn();

            builder.Property(r => r.ValorDiaria)
                .HasPrecision(7, 2);

            builder.Property(r => r.Status)
                .HasMaxLength(50);

            builder.Property(r => r.DataEntrada)
                .IsRequired();

            builder.Property(r => r.DataSaida)
                .IsRequired();
        }
    }
}
