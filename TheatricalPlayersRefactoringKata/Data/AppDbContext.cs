using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("DataSource=app.db;Cache=Shared;Foreign Keys=False");

            if (Debugger.IsAttached)
                options.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Play.OnModelCreating(builder);
            StatementLog.OnModelCreating(builder);
        }

        public DbSet<Play> Plays { get; set; }
        public DbSet<PlayTypes> PlayTypes { get; set; }
        public DbSet<StatementLog> StatementLogs { get; set; }
    }
}
