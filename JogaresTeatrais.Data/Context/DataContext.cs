using JogadoresTeatrais.Domain.Entities;
using JogaresTeatrais.Data.Extensions;
using JogaresTeatrais.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace JogaresTeatrais.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        #region
        public DbSet<Jogar>? Jogar { get; set; }

        public DbSet<Fatura>? Fatura { get; set; }

        public DbSet<Desempenho>? Desempenho { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JogarMap());
            modelBuilder.ApplyConfiguration(new DesempenhoMap());
            modelBuilder.ApplyConfiguration(new FaturaMap());
            

            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }

    }
}
