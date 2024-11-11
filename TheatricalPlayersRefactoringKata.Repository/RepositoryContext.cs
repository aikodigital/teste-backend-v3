﻿using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Model.Models;

namespace TheatricalPlayersRefactoringKata.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext() {}

    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    { }

    public DbSet<Performance> Performances { get; set; }

    public DbSet<Play> Plays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=theater.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Play>()
            .Property(p => p.Genre)
            .HasConversion<string>();
            
        base.OnModelCreating(modelBuilder);
    }
}
