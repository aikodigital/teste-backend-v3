using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Configurations;

public class CustomerStatementProcessConfiguration : IEntityTypeConfiguration<CustomerStatementProcess>
{
    public void Configure(EntityTypeBuilder<CustomerStatementProcess> builder)
    {
        builder.ToTable("CustomerStatementProcess");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnType("integer")
            .UseIdentityAlwaysColumn();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(t => t.CustomerStatementId)
            .HasColumnName("CustomerStatementId")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(t => t.Process)
            .HasColumnName("Process")
            .HasColumnType("boolean")
            .IsRequired();

        builder.HasOne(t => t.CustomerStatement)
               .WithMany(t => t.CustomerStatementProcess)
               .HasForeignKey(t => t.CustomerStatementId)
               .HasPrincipalKey(t => t.Id)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
