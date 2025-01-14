namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Statement
    {
        private string _namePlay { get; set; }
        private decimal _amountOwed { get; set; }
        private int _seats { get; set; }
        private int _earnedCredits { get; set; }
        
        public string NamePlay { get => _namePlay; set => _namePlay = value; }
        public decimal AmountOwed { get => _amountOwed; set => _amountOwed = value; }
        public int Seats { get => _seats; set => _seats = value; }
        public int EarnedCredits { get => _earnedCredits; set => _earnedCredits = value; }

        public Statement(string namePlay, decimal amountOwed, int seats, int earnedCredits)
        {
            this._namePlay = namePlay;
            this._amountOwed = amountOwed;
            this._seats = seats;
            this._earnedCredits = earnedCredits;
        }
    }
}
