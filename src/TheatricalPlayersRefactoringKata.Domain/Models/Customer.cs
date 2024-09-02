namespace TheatricalPlayersRefactoringKata.Domain.Models;

/// <summary>
/// Represents a customer in the theatrical domain.
/// </summary>
public class Customer : Entity
{
    // Parameterless constructor EF Core
    protected Customer() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="Customer"/> class.
    /// </summary>
    /// <param name="name">The name of the customer.</param>
    public Customer(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets the name of the customer.
    /// </summary>
    public string Name { get; private set; } = null!;
}

