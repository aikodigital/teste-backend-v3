using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Repository.Configuration.Mapping
{
    public class Map<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.LastModifiedDate).IsRequired();
            builder.Property(x => x.Active).IsRequired();
        }
    }
}