using System.Collections.Generic;
using System.Linq;

namespace TheatricalPlayersRefactoringKata;

public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    public string Customer => _customer;
    public IReadOnlyList<Performance> Performances => _performances.AsReadOnly();
    public decimal TotalAmount => _performances.Sum(p => p.Amount);
    public int VolumeCredits => _performances.Sum(p => p.GetCredits());

    public Invoice(string customer, List<Performance> performance)
    {
        _customer = customer;
        _performances = performance;
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
            Customer = Customer,
            AmountOwed = TotalAmount,
            EarnedCredits = VolumeCredits,
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
