using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Configurations;

public class InvoiceProcessConfiguration : IEntityTypeConfiguration<InvoiceProcess>
{
    public void Configure(EntityTypeBuilder<InvoiceProcess> builder)
    {
        builder.ToTable("InvoiceProcess");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnType("integer")
            .UseIdentityAlwaysColumn();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(t => t)
            .HasColumnName("Audience")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();
    }
}
