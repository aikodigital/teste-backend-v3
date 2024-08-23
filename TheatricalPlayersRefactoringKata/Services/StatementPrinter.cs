using System.Collections.Generic;
using System;
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

        if(statementFormatt.TryGetValue(type, out var statementType))
        {
             return statementType.StatementFormat(invoice, plays);
        }
       

        throw new ArgumentException ($"Unsupported format: {type}");
    }
}
