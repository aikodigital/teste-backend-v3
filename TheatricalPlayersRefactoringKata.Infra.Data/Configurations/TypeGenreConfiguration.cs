using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Configurations;

public class TypeGenreConfiguration : IEntityTypeConfiguration<TypeGenre>
{
    public void Configure(EntityTypeBuilder<TypeGenre> builder)
    {
        builder.ToTable("TypeGenre");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
                .HasColumnType("integer")
                .UseIdentityAlwaysColumn();

        builder.Property(t => t.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

        builder.Property(t => t.Name)
               .HasColumnName("Name")
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.BasePriceMultiplier)
               .HasColumnName("BasePriceMultiplier")
               .HasColumnType("integer")
               .HasDefaultValue(1m);

        builder.Property(t => t.MaxAudience)
               .HasColumnName("MaxAudience")
               .HasColumnType("integer");

        builder.Property(t => t.ExtraFeePerAudience)
               .HasColumnName("ExtraFeePerAudience")
               .HasColumnType("integer");

        builder.Property(t => t.BaseFeePerAudience)
               .HasColumnName("BaseFeePerAudience")
               .HasColumnType("integer");

        builder.Property(t => t.BonusFee)
               .HasColumnName("BonusFee")
               .HasColumnType("integer");

        builder.HasData(
            new TypeGenre { Id = 1, Name = "tragedy", BasePriceMultiplier = 10, MaxAudience = 30, ExtraFeePerAudience = 1000 },
            new TypeGenre { Id = 2, Name = "comedy", BasePriceMultiplier = 10, MaxAudience = 20, ExtraFeePerAudience = 500, BaseFeePerAudience = 300, BonusFee = 10000 },
            new TypeGenre { Id = 3, Name = "history", BasePriceMultiplier = 10 });
    }
}