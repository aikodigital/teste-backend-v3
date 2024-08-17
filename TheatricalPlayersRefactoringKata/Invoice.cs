using System;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata
{
    public class Invoice
    {
        public string Customer { get; }
        public List<Performance> Performances { get; }

        /// <summary>
        /// Represents an invoice for a series of theatrical performances.
        /// </summary>
        /// <param name="customer">Customer name</param>
        /// <param name="performances">List of performances</param>
        /// <exception cref="ArgumentException">Thrown when customer name is null, empty, or whitespace.</exception>
        /// <exception cref="ArgumentNullException">Thrown when performances list is null.</exception>
        public Invoice(string customer, List<Performance> performances)
        {
            if (string.IsNullOrWhiteSpace(customer))
                throw new ArgumentException("Customer cannot be null, empty, or whitespace.", nameof(customer));
            else if (performances == null)
                throw new ArgumentNullException(nameof(performances), "Performances list cannot be null");
            Customer = customer;
            Performances = performances;
        }
    }
}
