using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data.Configurations
{
    public class HospedeConfiguration : IEntityTypeConfiguration<Hospede>
    {
        public void Configure(EntityTypeBuilder<Hospede> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Nome)
                .HasMaxLength(100);

            builder.Property(h => h.RG)
                .HasMaxLength(12);

            builder.Property(h => h.CPF)
                .HasMaxLength(14);

            builder.Property(h => h.Endereco)
                .HasMaxLength(14);

            builder.Property(h => h.CEP)
                .HasMaxLength(8);

            builder.Property(h => h.Telefone)
                .HasMaxLength(14);

            builder.Property(h => h.Email)
                .HasMaxLength(70);

            builder.HasMany<Reserva>();
        }
    }

}
