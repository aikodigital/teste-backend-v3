using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("tb_invoices");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Customer)
                .HasColumnName("customer")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.TotalAmount)
                .HasColumnName("total_amount")
                .IsRequired();

            builder.Property(p => p.TotalCredits)
                .HasColumnName("total_credits")
                .IsRequired();

            builder.HasMany(inv => inv.Performances)
                .WithMany(per => per.Invoices)
                .UsingEntity(x => x.ToTable("tb_invoice_performance"));
        }
    }
}
