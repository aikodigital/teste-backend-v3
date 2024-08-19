namespace TheatricalPlayersRefactoringKata.Models
{
    public class Customer
    {
        private string _name;
        private double _credits;
        private double _partialCredits;

        public string Name { get => _name; set => _name = value; }
        public double Credits { get => _credits; set => _credits = value; }

        public double PartialCredits { get => _partialCredits; set => _partialCredits = value; }

        public Customer(string name, double credits, double partialCredits)
        {
            _name = name;
            _credits = credits;
            _partialCredits = partialCredits;
        }
    }
}
