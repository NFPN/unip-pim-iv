using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data.Configurations
{
    public class VagaEstacionamentoConfiguration : IEntityTypeConfiguration<VagaEstacionamento>
    {
        public void Configure(EntityTypeBuilder<VagaEstacionamento> builder)
        {
            builder.HasKey(ve => ve.Id);

            builder.Property(ve => ve.Placa)
                .HasMaxLength(7);

            builder.Property(ve => ve.NumeroVaga)
                .HasMaxLength(5);
        }
    }

}
