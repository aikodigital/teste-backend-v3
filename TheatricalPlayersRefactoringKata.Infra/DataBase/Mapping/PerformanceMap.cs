using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Infra.DataBase.Mapping
{
    public class PerformanceMap
    {
        public PerformanceMap(EntityTypeBuilder<Performance> tb) { 
        
            tb.HasKey(x => x.Id);
            tb.HasOne(x => x.Invoice).WithMany(x => x.Performances);
            tb.HasOne(x => x.Play).WithMany(x => x.Performances);
        }
    }
}
