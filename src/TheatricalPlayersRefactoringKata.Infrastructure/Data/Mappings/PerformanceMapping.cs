using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data.Mappings;

public class PerformanceMapping : IEntityTypeConfiguration<Performance>
{
    public void Configure(EntityTypeBuilder<Performance> builder)
    {
        builder
            .ToTable("Performances");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("PerformanceId");

        builder.HasOne(x => x.Play)
            .WithMany()
            .HasForeignKey("PlayId")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(x => x.Audience)
            .IsRequired();

        builder
            .Property(x => x.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(x => x.Credits)
            .IsRequired();
    }
}