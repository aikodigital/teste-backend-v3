using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Formatters;
using TheatricalPlayersRefactoringKata.Interfaces;
namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
     private readonly Dictionary<string, IStatementStrategy> statementFormatt;

    public StatementPrinter()
    {

       statementFormatt = new Dictionary<string, IStatementStrategy>()
       {
           { "text", new StatementText() },
           { "xml", new StatementXml() }
        };
    } 
        
        
    public string Print(Invoice invoice, Dictionary<string, Play> plays, string type)
    {
        var statementType = statementFormatt[type].StatementFormat(invoice, plays);
        return statementType;
    }
}
