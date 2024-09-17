using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Invoice
    {
        public Invoice(Customer customer,
        IDictionary<string, Performance> performances)
        {
            Customer = customer;
            Performances = performances;
        }
        public Customer Customer { get; set; }
        public IDictionary<string, Performance> Performances { get; set; }

    }
}
