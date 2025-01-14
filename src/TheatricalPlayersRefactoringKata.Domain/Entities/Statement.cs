namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Statement
    {
        private string _customer { get; set; }
        private decimal _amountOwed { get; set; }
        private int _seats { get; set; }
        private int _earnedCredits { get; set; }
        
        public string Customer { get => _customer; set => _customer = value; }
        public decimal AmountOwed { get => _amountOwed; set => _amountOwed = value; }
        public int Seats { get => _seats; set => _seats = value; }
        public int EarnedCredits { get => _earnedCredits; set => _earnedCredits = value; }

        public Statement(string customer, decimal amountOwed, int seats, int earnedCredits)
        {
            this._customer = customer;
            this._amountOwed = amountOwed;
            this._seats = seats;
            this._earnedCredits = earnedCredits;
        }
    }
}
