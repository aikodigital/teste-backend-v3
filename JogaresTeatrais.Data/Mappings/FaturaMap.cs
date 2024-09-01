using JogadoresTeatrais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace JogaresTeatrais.Data.Mappings
{
    public class FaturaMap : IEntityTypeConfiguration<Fatura>
    {
        public void Configure(EntityTypeBuilder<Fatura> builder)
        {
            builder.Property(x => x.Id).IsRequired();

        }

    }
}
