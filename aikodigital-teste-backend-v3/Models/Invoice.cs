using System.ComponentModel.DataAnnotations;

namespace aikodigital_teste_backend_v3.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public required string Customer { get; set; } 
        public required List<Performance> Performances { get; set; } 

        public Invoice() 
        {
        }

        public Invoice(string customer, List<Performance> performances)
        {
            Customer = customer;
            Performances = performances;
        }
    }
}