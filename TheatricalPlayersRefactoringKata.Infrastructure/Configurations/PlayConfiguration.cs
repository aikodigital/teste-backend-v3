using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Configurations
{
    public class PlayConfiguration : IEntityTypeConfiguration<Play>
    {
        public void Configure(EntityTypeBuilder<Play> builder)
        {
            builder.ToTable("tb_plays");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Lines)
                .HasColumnName("lines")
                .IsRequired();

            builder.Property(p => p.Genre)
                .HasColumnName("genre")
                .IsRequired();
        }
    }
}
