using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Configurations;

public class CustomerStatementConfiguration : IEntityTypeConfiguration<CustomerStatement>
{
    public void Configure(EntityTypeBuilder<CustomerStatement> builder)
    {
        builder.ToTable("CustomerStatement");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnType("integer")
            .UseIdentityAlwaysColumn();

        builder.Property(t => t.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

        builder.Property(cs => cs.Customer)
               .HasColumnName("Customer")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(cs => cs.TotalAmount)
               .HasColumnName("TotalAmount")
               .HasColumnType("integer")
               .IsRequired();

        builder.Property(cs => cs.VolumeCredits)
               .HasColumnName("VolumeCredits")
               .HasColumnType("integer")
               .IsRequired();

        builder.HasMany(cs => cs.CustomerPlaysStatement)
               .WithOne(cps => cps.CustomerStatement)
               .HasForeignKey(cps => cps.CustomerStatementId)
               .OnDelete(DeleteBehavior.Cascade); 
    }
}
