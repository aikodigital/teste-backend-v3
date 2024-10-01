using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Configurations;

public class PlayConfiguration : IEntityTypeConfiguration<Play>
{
    public void Configure(EntityTypeBuilder<Play> builder)
    {
        builder.ToTable("Play");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnType("integer")
            .UseIdentityAlwaysColumn();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(t => t.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(t => t.TypeGenreId)
            .HasColumnName("TypeGenreId")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(t => t.Lines)
            .HasColumnName("Lines")
            .HasColumnType("integer")
            .IsRequired();

        builder.HasMany(t => t.Performances)
                   .WithOne(t => t.Play)
                   .HasForeignKey(t => t.PlayId)
                   .HasPrincipalKey(t => t.Id)
                   .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.TypeGenre)
                   .WithMany(t => t.Plays)
                   .HasForeignKey(t => t.TypeGenreId)
                   .HasPrincipalKey(t => t.Id)
                   .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Play { Id = 1, Name = "Hamlet", Lines = 4024, TypeGenreId = 1 },
            new Play { Id = 2, Name = "As You Like It", Lines = 2670, TypeGenreId = 2 },
            new Play { Id = 3, Name = "Othello", Lines = 3560, TypeGenreId = 1 },
            new Play { Id = 4, Name = "Henry V", Lines = 3227, TypeGenreId = 3 },
            new Play { Id = 5, Name = "King John", Lines = 2648, TypeGenreId = 3 },
            new Play { Id = 6, Name = "Richard III", Lines = 3718, TypeGenreId = 3 });
    }
}