using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Configurations
{
    public class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
    {
        public void Configure(EntityTypeBuilder<Performance> builder)
        {
            builder.ToTable("tb_performances");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Audience)
                .HasColumnName("audience")
                .IsRequired();

            builder.Property(p => p.Credits)
                .HasColumnName("credits")
                .IsRequired();

            builder.HasOne(per => per.Play)
                .WithMany(p => p.Performances)
                .HasForeignKey(per => per.PlayId);

            builder.HasMany(per => per.Invoices)
                .WithMany(inv => inv.Performances)
                .UsingEntity(x => x.ToTable("tb_invoice_performance"));
        }
    }
}
