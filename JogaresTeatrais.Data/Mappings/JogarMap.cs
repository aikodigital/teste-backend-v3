using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JogaresTeatrais.Data.Mappings
{
    public class JogarMap : IEntityTypeConfiguration<Jogar>
    {
        public void Configure(EntityTypeBuilder<Jogar> builder)
        {
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();
        }
    }
}
