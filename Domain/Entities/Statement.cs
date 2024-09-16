namespace TheatricalPlayersRefactoringKata
{
    public class Statement
    {
        public string Customer { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
    }

    public class Item
    {
        public string PlayName { get; set; }
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
        public int Seats { get; set; }
    }
}
