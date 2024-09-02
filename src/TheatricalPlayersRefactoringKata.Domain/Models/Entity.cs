namespace TheatricalPlayersRefactoringKata.Domain.Models;

/// <summary>
/// Represents the base class for all entities in the domain.
/// </summary>
public abstract class Entity
{
    private Guid _id;

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// Generates a new Guid if the Id is empty.
    /// </summary>
    protected Entity()
    {
        _id = Guid.NewGuid();
    }

    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id
    {
        get => _id;
        set
        {
            if (_id == Guid.Empty)
            {
                _id = value == Guid.Empty ? Guid.NewGuid() : value;
            }
        }
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    public static bool operator ==(Entity? a, Entity? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    /// <summary>
    /// Determines whether two entities are not equal.
    /// </summary>
    public static bool operator !=(Entity? a, Entity? b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// </summary>
    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    /// <summary>
    /// Returns a string that represents the current entity.
    /// </summary>
    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
}

