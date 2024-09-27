using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
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
                   .HasPrincipalKey(t => t.Id);


        builder.HasOne(t => t.TypeGenre)
                   .WithMany(t => t.Plays)
                   .HasForeignKey(t => t.TypeGenreId)
                   .HasPrincipalKey(t => t.Id);
    }
}