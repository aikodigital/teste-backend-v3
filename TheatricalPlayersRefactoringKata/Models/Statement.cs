using System;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public int VolumeCredits { get; set; }
        public List<StatementLine> Lines { get; set; }

        public Statement()
        {
            Lines = new List<StatementLine>();
        }

        /// <summary>
        /// Represents a statement for a customer
        /// </summary>
        /// <param name="customer">Customer name</param>
        public Statement(string customer)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Lines = new List<StatementLine>();
        }
    }

    public class StatementLine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Seats { get; set; }
        public int Credits { get; set; }

        /// <summary>
        /// Represents a line in a statement
        /// </summary>
        /// <param name="name">Name of the play</param>
        /// <param name="amount">Amount of the play</param>
        /// <param name="seats">Number of occupied seats</param>
        public StatementLine(string name, decimal amount, int credits, int seats)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Amount = amount;
            Seats = seats;
            Credits = credits;
        }
    }
}
