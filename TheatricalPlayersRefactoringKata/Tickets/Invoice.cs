using System.Collections.Generic;
using System.Linq;
using TheatricalPlayersRefactoringKata.Performances;
using TheatricalPlayersRefactoringKata.Dtos;

namespace TheatricalPlayersRefactoringKata.Tickets;

public class Invoice
{
    private readonly string _customer;
    private readonly List<Performance> _performances;
    
    public decimal AmountOwed => _performances.Sum(p => p.Amount);
    public int EarnedCredits => _performances.Sum(p => p.GetCredits());

    public Invoice(string customer, List<Performance> performances)
    {
        _customer = customer;
        _performances = performances;
    }

    public void Calculute()
    {
        _performances.ForEach(p => p.CalculateAmount());
    }

    public List<Performance> GetPerformancesByName(string name)
    {
        return _performances.Where(p => p.PlayName == name).ToList();
    }

    public StatementDto GetStatementDto()
    {
        return new StatementDto
        {
            Customer = _customer,
            AmountOwed = AmountOwed,
            EarnedCredits = EarnedCredits,
            Items = _performances.Select(p => new StatementItemDto
            {
                Name = p.PlayName,
                AmountOwed = p.Amount,
                EarnedCredits = p.GetCredits(),
                Seats = p.Audience

            }).ToList()
        };
    }

}
