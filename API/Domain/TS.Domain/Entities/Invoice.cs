using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TS.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public DateTime CreationAt { get; set; }
        [Required]
        [ForeignKey(nameof(Customer))]
        public long CustomerId { get; set; }
        [Required]
        public long PlayId { get; set; }
        [Required]
        public decimal LoyaltyCredit { get; set; }
    }
}