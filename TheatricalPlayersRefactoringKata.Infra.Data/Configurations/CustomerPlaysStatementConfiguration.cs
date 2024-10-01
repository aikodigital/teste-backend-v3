using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Configurations;

public class CustomerPlaysStatementConfiguration : IEntityTypeConfiguration<CustomerPlaysStatement>
{
    public void Configure(EntityTypeBuilder<CustomerPlaysStatement> builder)
    {
        builder.ToTable("CustomerPlaysStatement");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnType("integer")
            .UseIdentityAlwaysColumn();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(t => t.PlayId)
               .HasColumnName("PlayId")
               .HasColumnType("integer")
               .IsRequired();

        builder.Property(t => t.Amount)
               .HasColumnName("Amount")
               .HasColumnType("integer")
               .IsRequired();

        builder.Property(t => t.TotalSeats)
               .HasColumnName("TotalSeats")
               .HasColumnType("integer")
               .IsRequired();

        builder.HasOne(t => t.CustomerStatement)
               .WithMany(t => t.CustomerPlaysStatement)
               .HasForeignKey(t => t.CustomerStatementId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Play)
               .WithMany(t => t.CustomerPlaysStatement)
               .HasForeignKey(t => t.PlayId)
               .HasPrincipalKey(t => t.Id)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
