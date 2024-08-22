using System;

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

        public Customer(string name, double credits)
        {
            _name = name.Length >= 3 ? name : throw new ArgumentException("Name should be greater than 3 characters");
            _credits = credits >= 0 ? credits : throw new ArgumentException("Credits should be greater than 0");
            _partialCredits = 0;
        }
    }
}
