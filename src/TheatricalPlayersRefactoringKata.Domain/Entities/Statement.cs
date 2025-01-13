namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Statement
    {
        public string name { get; set; }
        public decimal amount { get; set; }
        public int seat { get; set; }
        public int credit { get; set; }
        public Statement(string name, decimal amount, int seat, int credit)
        {
            this.name = name;
            this.amount = amount;
            this.seat = seat;
            this.credit = credit;
        }
    }
}
