namespace TheatricalPlayersRefactoringKata.Core.Entities
{
    public class Play
    {
        public string Name { get; }
        public decimal Price { get; }
        public string Type { get; }

        public Play(string name, decimal price, string type)
        {
            Name = name;
            Price = price;
            Type = type;
        }
    }
}