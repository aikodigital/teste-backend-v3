using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities;

public class Invoice : AuditableEntity, IHasDomainEvent
{
    private int Id { get; set; }
    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

    public Invoice(string customer, List<Performance> performance)
    {
        _customer = customer;
        _performances = performance;
    }

}
