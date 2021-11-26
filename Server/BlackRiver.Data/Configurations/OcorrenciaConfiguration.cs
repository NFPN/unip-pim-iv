using BlackRiver.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackRiver.Data
{
    public class OcorrenciaConfiguration : IEntityTypeConfiguration<Ocorrencia>
    {
        public void Configure(EntityTypeBuilder<Ocorrencia> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .UseIdentityColumn();

            builder.Property(o => o.Departamento)
                .HasMaxLength(30);

            builder.Property(o => o.Descricao)
                .HasMaxLength(30);

            builder.Property(q => q.Score)
                .HasPrecision(2, 1);
        }
    }
}
