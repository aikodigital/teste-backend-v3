using JogadoresTeatrais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogaresTeatrais.Data.Mappings
{
    public class DesempenhoMap : IEntityTypeConfiguration<Desempenho>
    {
        public void Configure(EntityTypeBuilder<Desempenho> builder)
        {
            builder.Property(x => x.Id).IsRequired();
        }
    }
}
