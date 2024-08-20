using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;

public class PlayEntityTypeConfiguration : IEntityTypeConfiguration<PlayEntity>
{
    public void Configure(EntityTypeBuilder<PlayEntity> builder)
    {
        builder.ToTable("Plays");

        // (Name) Primary key
        builder.HasKey(entity => entity.Name);

        // (Name) Unique index
        builder.HasIndex(entity => entity.Name)
            .IsUnique();

        // (Name) Properties
        builder.Property(entity => entity.Name)
            .IsRequired()
            .HasMaxLength(255);

        // (Lines) Properties
        builder.Property(entity => entity.Lines)
            .IsRequired();

        // (Type) Properties
        builder.Property(entity => entity.Type)
            .IsRequired()
            .HasMaxLength(255);
    }
}