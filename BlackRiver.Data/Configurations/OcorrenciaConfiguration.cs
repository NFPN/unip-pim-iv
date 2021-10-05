using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data.Configurations
{
    public class OcorrenciaConfiguration : IEntityTypeConfiguration<Ocorrencia>
    {
        public void Configure(EntityTypeBuilder<Ocorrencia> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Departamento)
                .HasMaxLength(30);
        }
    }

}
