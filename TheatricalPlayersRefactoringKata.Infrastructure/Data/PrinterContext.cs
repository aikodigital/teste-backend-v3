using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.DTO;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data
{
    public class PrinterContext : DbContext {

        public PrinterContext(DbContextOptions<PrinterContext> options) : base(options) { }

        public DbSet<StatementDTO> Statement { get; set; }

    }
}
