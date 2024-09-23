using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;
namespace TheatricalPlayersRefactoringKata;
public class StatementInput
{    
    private Invoice _invoice;
    private Dictionary<string, Play> _plays;
    public Invoice invoice { get =>_invoice;set => _invoice = value ;}
    public Dictionary<string, Play> plays { get => _plays ; set => _plays = value; }

    public StatementInput(Invoice invoice, Dictionary<string, Play> plays)
    {
        _invoice = invoice;
        _plays = plays;
    }    
}