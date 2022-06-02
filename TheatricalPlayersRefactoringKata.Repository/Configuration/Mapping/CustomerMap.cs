using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Repository.Configuration.Mapping
{
    public class CustomerMap : Map<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(x => x.Invoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}