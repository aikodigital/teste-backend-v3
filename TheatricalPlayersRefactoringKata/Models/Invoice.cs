using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

public class Invoice
{
    public int Id { get; set; }
    public string Customer { get; set; }
    public List<Performance> Performances { get; set; } = new List<Performance>(); 


    public Invoice() { }


    public Invoice(string customer, List<Performance> performances)
    {
        Customer = customer;
        Performances = performances;
    }
}
