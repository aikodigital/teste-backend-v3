using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.PerformanceHistory;

public class PerformanceHistoryEntityTypeConfiguration : IEntityTypeConfiguration<PerformanceHistoryEntity>
{
    public void Configure(EntityTypeBuilder<PerformanceHistoryEntity> builder)
    {
        builder.ToTable("PerformanceHistory");

        // (Id) Primary key
        builder.HasKey(entity => entity.Id);

        // (Id) Auto-increment
        builder.Property(entity => entity.Id)
            .ValueGeneratedOnAdd();

        // (PlayId) Properties
        builder.Property(entity => entity.PlayId)
            .IsRequired()
            .HasMaxLength(255);

        // (Audience) Properties
        builder.Property(entity => entity.Audience)
            .IsRequired();

        // (AmountOwed) Properties
        builder.Property(entity => entity.AmountOwed)
            .IsRequired();

        // (EarnedCredits) Properties
        builder.Property(entity => entity.EarnedCredits)
            .IsRequired();

        // (InvoiceHistoryId) Reference
        builder.HasOne(entity => entity.InvoiceHistory)
            .WithMany(index => index.PerformancesHistories)
            .HasForeignKey(entity => entity.InvoiceHistoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}