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
        public Dictionary<string, Play> Plays { get; private set; }
        public List<Performance> Performances { get; private set; }

        public Invoice()
        {
            Plays = new Dictionary<string, Play>();
            Performances = new List<Performance>();
        }

        [JsonConstructor]
        public Invoice(string customer, Dictionary<string, Play> plays, List<Performance> performances)
        {
            Id = Guid.NewGuid();
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Plays = plays ?? new Dictionary<string, Play>();
            Performances = performances ?? new List<Performance>();
        }

        public decimal TotalAmount { get; set; }
        public int TotalCredits { get; set; }
    }
}