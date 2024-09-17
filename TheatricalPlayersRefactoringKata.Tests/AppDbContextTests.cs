using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;
using Xunit;
using SQLitePCL;

namespace TheatricalPlayersRefactoringKata.Tests;

public class AppDbContextTests : IDisposable
{
    private readonly AppDbContext _context;

    public AppDbContextTests()
    {
        // Inicializa o SQLitePCL se necessário
        raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _context = new AppDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();
    }

    [Fact]
    public void CanAddStatementWithItems()
    {
        // Arrange
        var statement = new Statement
        {
            Customer = "BigCo",
            TotalAmountOwed = 3995.4m,
            TotalEarnedCredits = 56,
            Items = new List<Item>
        {
            new Item { AmountOwed = 650, EarnedCredits = 25, Seats = 55 },
            new Item { AmountOwed = 547, EarnedCredits = 12, Seats = 35 }
        }
        };

        _context.Statements.Add(statement);
        _context.SaveChanges();

        var statements = _context.Statements.Include(s => s.Items).ToList();
        var addedStatement = statements.First();

        Assert.Equal("BigCo", addedStatement.Customer);
        Assert.Equal(2, addedStatement.Items.Count);
        Assert.Equal(3995.4m, addedStatement.TotalAmountOwed);
    }

    public void Dispose()
    {
        _context.Database.CloseConnection();
        _context.Dispose();
    }
}
