using JogaresTeatrais.Data.Extensions;
using JogaresTeatrais.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogaresTeatrais.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        #region
        public DbSet<Jogar> Jogar { get; set; }
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
