using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;

public class PlayEntityTypeConfiguration : IEntityTypeConfiguration<PlayEntity>
{
    public void Configure(EntityTypeBuilder<PlayEntity> builder)
    {
        builder.ToTable("Plays");

        // (Name) Primary key
        builder.HasKey(play => play.Name);

        // (Name) Unique index
        builder.HasIndex(play => play.Name)
            .IsUnique();

        // (Name) Properties
        builder.Property(play => play.Name)
            .IsRequired()
            .HasMaxLength(255);

        // (Lines) Properties
        builder.Property(play => play.Lines)
            .IsRequired();

        // (Type) Properties
        builder.Property(play => play.Type)
            .IsRequired()
            .HasMaxLength(255);
    }
}