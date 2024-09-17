using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class InvoiceTests
{
    [Fact]
    public void CriacaoValida()
    {
        var customer = "Jonny Deep";
        var performances = new List<Performance> { new Performance("Hamlet", 5) };
        var invoice = new Invoice(customer, performances);

        Assert.Equal(customer, invoice.Customer);
        Assert.Equal(performances, invoice.Performances);
    }

    [Fact]
    public void AlteracaoDeDados()
    {
        var invoice = new Invoice("Jonny Deep", new List<Performance>());
        invoice.Customer = "Joseph Mary";

        Assert.Equal("Joseph Mary", invoice.Customer);
    }
}