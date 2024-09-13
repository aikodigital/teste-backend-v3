using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Entities;

public class InvoiceEntity
{
    public string Customer { get; private set; }

    public List<PerformanceEntity> Performances { get; private set; }

    public InvoiceEntity(string customer, List<PerformanceEntity> performances)
    {
        Customer = customer;
        Performances = performances;
    }
}