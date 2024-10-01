using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Configurations;

public class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
{
    public void Configure(EntityTypeBuilder<Performance> builder)
    {
        builder.ToTable("Performance");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
                .HasColumnType("integer")
                .UseIdentityAlwaysColumn();

        builder.Property(t => t.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

        builder.Property(t => t.Audience)
                .HasColumnName("Audience")
                .HasColumnType("integer")
                .IsRequired();

        builder.HasOne(t => t.Play)
               .WithMany(t => t.Performances)
               .HasForeignKey(t => t.PlayId)
               .HasPrincipalKey(t => t.Id)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
