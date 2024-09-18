using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data
{
    /// <summary>
    /// Represents the Entity Framework Core database context for the application.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Configures the database context to use SQLite with a database file located in the application's base directory.
        /// </summary>
        /// <param name="optionsBuilder">The options builder used to configure the database context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TheatricalPlayersRefactoringKata.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        /// <summary>
        /// Gets or sets the DbSet for <see cref="Invoice"/> entities.
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for <see cref="InvoiceCalculeteSettings"/> entities.
        /// </summary>
        public DbSet<InvoiceCalculeteSettings> InvoiceCalculeteSettings { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for <see cref="InvoiceCreditSettings"/> entities.
        /// </summary>
        public DbSet<InvoiceCreditSettings> InvoiceCreditSettings { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for <see cref="Performance"/> entities.
        /// </summary>
        public DbSet<Performance> Performances { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for <see cref="Play"/> entities.
        /// </summary>
        public DbSet<Play> Play { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for <see cref="PlayType"/> entities.
        /// </summary>
        public DbSet<PlayType> PlayType { get; set; }
    }
}
