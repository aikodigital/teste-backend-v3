using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    [Key]
    public int Id { get; set; }
    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice(string customer, List<Performance> performances)
    {
        _customer = customer;
        _performances = performances;
    }
    public Invoice() { }
}
