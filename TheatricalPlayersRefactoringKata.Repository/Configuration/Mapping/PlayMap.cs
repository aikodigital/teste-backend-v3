using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Repository.Configuration.Mapping
{
    public class PlayMap : Map<Play>
    {
        public override void Configure(EntityTypeBuilder<Play> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Lines).IsRequired();
            builder.Property(x => x.PlayType).IsRequired();
        }
    }
}