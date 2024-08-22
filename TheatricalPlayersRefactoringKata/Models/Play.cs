namespace TheatricalPlayersRefactoringKata.Models
{
    public class Play
    {
        public string Name { get; }
        public int Cost { get; }
        public string Type { get; }

        public Play(string name, int cost, string type)
        {
            Name = name;
            Cost = cost;
            Type = type;
        }
    }
}
