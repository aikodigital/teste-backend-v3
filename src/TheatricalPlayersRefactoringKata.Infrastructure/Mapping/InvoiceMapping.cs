using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Mapping
{
    internal class InvoiceMapping : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable(nameof(Invoice));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Customer)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.HasMany(e => e.Performances)
                .WithOne(e => e.Invoice)
                .HasForeignKey(e => e.InvoiceId);
        }
    }
}
