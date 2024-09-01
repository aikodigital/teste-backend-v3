using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Invoice
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Customer;
    public List<Performance> Performances;

    public Invoice()
    {
        
    }

    public Invoice(string customer, List<Performance> performance)
    {
        Customer = customer;
        Performances = performance;
    }

}
