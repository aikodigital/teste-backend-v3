using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Mapping
{
    internal class PerformanceMapping : IEntityTypeConfiguration<Performance>
    {
        public void Configure(EntityTypeBuilder<Performance> builder)
        {
            builder.ToTable(nameof(Performance));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.PlayId)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Audience)
                .IsRequired();
        }
    }
}
