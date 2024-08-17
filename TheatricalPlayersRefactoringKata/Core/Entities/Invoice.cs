using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace TheatricalPlayersRefactoringKata.Core.Entities
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public string Customer { get; private set; }
        public List<Performance> Performances { get; private set; }

        public Invoice()
        {
            Performances = new List<Performance>();
        }

        [JsonConstructor]
        public Invoice(string customer, List<Performance> performances)
        {
            Id = Guid.NewGuid();
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Performances = performances ?? new List<Performance>();
        }

        public decimal TotalAmount => Performances.Sum(p => p.Audience * p.Play.Price);

        public int TotalCredits => Performances.Sum(p => Math.Max(p.Audience - 30, 0));
    }
}