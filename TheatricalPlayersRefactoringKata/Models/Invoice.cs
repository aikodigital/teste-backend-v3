using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string Customer { get; set; }
        public List<Performance> Performances { get; set; }

        public Invoice()
        {
            Performances = new List<Performance>();
        }

        public Invoice(string customer, List<Performance> performances)
        {
            Customer = customer;
            Performances = performances;
        }
    }
}
