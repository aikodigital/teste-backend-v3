using JogadoresTeatrais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JogaresTeatrais.Data.Mappings
{
    public class DesempenhoMap : IEntityTypeConfiguration<Desempenho>
    {
        public void Configure(EntityTypeBuilder<Desempenho> builder)
        {
            builder.Property(x => x.Id).IsRequired();
        }
    }
}
