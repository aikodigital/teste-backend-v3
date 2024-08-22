using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

public class Invoice
{
    public int Id { get; set; }
    public string Customer { get; set; }
    public List<Performance> Performances { get; set; } = new List<Performance>(); // Definindo a lista com um valor padrão

    // Construtor padrão necessário para o Entity Framework Core
    public Invoice() { }

    // Construtor adicional se necessário
    public Invoice(string customer, List<Performance> performances)
    {
        Customer = customer;
        Performances = performances;
    }
}
