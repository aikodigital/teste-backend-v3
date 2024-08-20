using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.InvoiceHistory;

public class InvoiceHistoryEntityTypeConfiguration : IEntityTypeConfiguration<InvoiceHistoryEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceHistoryEntity> builder)
    {
        builder.ToTable("InvoiceHistory");

        // (Id) Primary key
        builder.HasKey(entity => entity.Id);

        // (Id) Auto-increment
        builder.Property(entity => entity.Id)
            .ValueGeneratedOnAdd();

        // (Customer) Properties
        builder.Property(entity => entity.Customer)
            .IsRequired()
            .HasMaxLength(255);

        // (TotalAmountOwed) Properties
        builder.Property(entity => entity.TotalAmountOwed)
            .IsRequired();

        // (TotalEarnedCredits) Properties
        builder.Property(entity => entity.TotalEarnedCredits)
            .IsRequired();

        // (DateOfInvoice) Properties
        builder.Property(entity => entity.DateOfInvoice)
            .HasDefaultValue("GETDATE()");

        // (PerformancesHistories) Reference
        builder.HasMany(entity => entity.PerformancesHistories)
            .WithOne(index => index.InvoiceHistory)
            .HasForeignKey(entity => entity.InvoiceHistoryId);
    }
}