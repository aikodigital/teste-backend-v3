using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;


namespace TheatricalPlayersRefactoringKata.Presenters;
public class StatementPresenter 
{
    
    private readonly Invoice _invoice;
    private readonly List<StatementPerfomance> _perfomances;

    public List<StatementPerfomance> Perfomances {
        get => _perfomances;
    }

    public string Customer {
        get => _invoice.Customer;
    }

    public decimal TotalCharge {
        get => (decimal) _invoice.TotalCharge / 100;
    } 

    public int TotalCredits {
        get => _invoice.TotalCredits;
    }

    public StatementPresenter(Invoice invoice) {
        _invoice = invoice;
        _perfomances = [];
        foreach (Performance perf in invoice.Performances) {
            _perfomances.Add(new StatementPerfomance(perf));
        }
    }
}

public class StatementPerfomance(Performance perfomance)
{   
    private Performance _perfomance = perfomance;

    public string PlayName {
        get => _perfomance.Play.Name;
    } 

    public int Audience {
        get => _perfomance.Audience;
    } 

    public decimal Charge {
        get => (decimal) _perfomance.Charge / 100;
    }
}