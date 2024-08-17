using System;
using System.Collections.Generic;
using System.Linq;

namespace TheatricalPlayersRefactoringKata.Core.Entities
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public string Customer { get; private set; }
        public List<Performance> Performances { get; private set; }

        public Invoice(string customer, List<Performance> performances)
        {
            Id = Guid.NewGuid();
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Performances = performances ?? new List<Performance>();
        }

        public void AddPerformance(Performance performance)
        {
            Performances.Add(performance);
        }

        public void RemovePerformance(Performance performance)
        {
            Performances.Remove(performance);
        }

        public decimal TotalAmount => Performances.Sum(p => p.Audience * 100);

        public int TotalCredits => Performances.Sum(p => Math.Max(p.Audience - 30, 0));
    }
}