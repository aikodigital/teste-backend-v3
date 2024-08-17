using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Models;

public class Invoice
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Customer { get; set; }

    public List<Performance> Performances { get; set; } = new List<Performance>();

    public Invoice(string customer, List<Performance> performances)
    {
        Customer = customer;
        Performances = performances;
    }

}
