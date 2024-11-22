using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entity;

namespace Infrastructure
{
    public class TesteBackendV3DbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Performance> Performances { get; set; }

        public TesteBackendV3DbContext(DbContextOptions options) : base(options) { }
    }
}
