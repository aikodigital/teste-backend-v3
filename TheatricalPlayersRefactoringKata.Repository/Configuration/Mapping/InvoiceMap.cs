using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Repository.Configuration.Mapping
{
    public class InvoiceMap : Map<Invoice>
    {
        public override void Configure(EntityTypeBuilder<Invoice> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Credits).IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(c => c.CustomerId)
                .HasPrincipalKey(x => x.Id);

            builder.HasMany(x => x.Performances)
                .WithOne(i => i.Invoice)
                .HasForeignKey(i => i.InvoiceId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}