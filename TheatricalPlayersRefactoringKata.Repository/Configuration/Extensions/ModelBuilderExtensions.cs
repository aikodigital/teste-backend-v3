using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Repository.Configuration.Mapping;

namespace TheatricalPlayersRefactoringKata.Repository.Configuration.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfigurationMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new InvoiceMap());
            modelBuilder.ApplyConfiguration(new PerformanceMap());
            modelBuilder.ApplyConfiguration(new PlayMap());
        }
    }
}