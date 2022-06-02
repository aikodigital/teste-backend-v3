using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Repository.Configuration.Mapping
{
    public class PerformanceMap : Map<Performance>
    {
        public override void Configure(EntityTypeBuilder<Performance> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.PlayId).IsRequired();
            builder.Property(x => x.InvoiceId).IsRequired();
            builder.Property(x => x.Audience).IsRequired();

            builder.HasOne(x => x.Play)
               .WithMany(c => c.Performances)
               .HasForeignKey(c => c.PlayId)
               .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Invoice)
               .WithMany(c => c.Performances)
               .HasForeignKey(c => c.InvoiceId)
               .HasPrincipalKey(x => x.Id);
        }
    }
}