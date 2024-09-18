using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Context
{
    public class AppSqlLiteContext : DbContext
    {

        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<PerformanceEntity> Performances { get; set; }

        public DbSet<PlayEntity> Plays { get; set; }
        public DbSet<TheaterPlayEntity> TheaterPlays { get; set; }
        public DbSet<ReportEntity> Reports { get; set; }

        public AppSqlLiteContext(DbContextOptions<AppSqlLiteContext> options)
       : base(options)
        {
           
        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }   
}
