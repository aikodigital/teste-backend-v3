namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = string.Empty;
    }
}
