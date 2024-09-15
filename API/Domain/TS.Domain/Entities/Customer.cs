using System.ComponentModel.DataAnnotations;

namespace TS.Domain.Entities
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public decimal LoyaltyCredit { get; set; }
    }
}