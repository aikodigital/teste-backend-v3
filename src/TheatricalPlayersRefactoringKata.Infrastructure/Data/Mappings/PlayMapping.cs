using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data.Mappings;

public class PlayMapping : IEntityTypeConfiguration<Play>
{
    public void Configure(EntityTypeBuilder<Play> builder)
    {
        builder
            .ToTable("Plays");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("PlayId");

        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Genre)
            .IsRequired();

        builder
            .Property(x => x.Lines)
            .IsRequired();
    }
}