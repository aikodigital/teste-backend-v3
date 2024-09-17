using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Context;
using TheatricalPlayersRefactoringKata.Interface;
using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Service;
public class StatementProcessingService
{
    private readonly IStatementFormatter _formatter;
    private readonly Dictionary<string, Play> _plays;
    private readonly TheaterDbContext _dbContext;

    public StatementProcessingService()
    {
            
    }
    public StatementProcessingService(IStatementFormatter formatter, Dictionary<string, Play> plays, TheaterDbContext dbContext)
    {
        _formatter = formatter;
        _plays = plays;
        _dbContext = dbContext;
    }

    public async Task<string> ProcessInvoiceAsync(Invoice invoice)
    {
        // Assuming your formatter and plays are already set up
        string statement = await _formatter.FormatAsync(invoice, _plays);

        // Save the result in the database
        //var statementRecord = new Invoice { InvoiceId = invoice.Id, XmlStatement = statement };
        //_dbContext.Invoices.Add(statementRecord);
        //await _dbContext.SaveChangesAsync();

        return statement;
    }
}

