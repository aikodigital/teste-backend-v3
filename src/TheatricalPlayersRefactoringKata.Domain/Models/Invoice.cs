using TheatricalPlayersRefactoringKata.Domain.Models.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.Models;

/// <summary>
/// Represents an invoice in the theatrical domain.
/// </summary>
public class Invoice : Entity, IAggregateRoot
{
    // Parameterless constructor EF Core
    protected Invoice() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="Invoice"/> class.
    /// </summary>
    /// <param name="customer">The customer associated with the invoice.</param>
    /// <param name="performances">The performances associated with the invoice.</param>
    public Invoice(Customer customer, ICollection<Performance> performances)
    {
        Customer = customer;
        Performances = performances;
    }

    /// <summary>
    /// Gets the customer associated with the invoice.
    /// </summary>
    public Customer Customer { get; private set; } = null!;

    /// <summary>
    /// Gets the list of performances included in the invoice.
    /// </summary>
    public ICollection<Performance> Performances { get; private set; } = null!;
}
