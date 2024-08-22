using System;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public double Credits { get; set; }
        public double PartialCredits { get; set; }

        public Customer(string name, double credits)
        {
            Name = name.Length >= 3 ? name : throw new ArgumentException("Name must be greater than 3 characters");
            Credits = credits >= 0 ? credits : throw new ArgumentException("Credits must be greater than 0");
            PartialCredits = 0;
        }
    }
}
