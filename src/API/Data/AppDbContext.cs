using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using TheatricalPlayersRefactoringKata;

namespace Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Statement> Statements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
}
