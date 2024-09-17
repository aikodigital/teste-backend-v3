using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TS.Domain.Entities
{
    public class Customer
    {
        public Customer()
        {
            Invoices = [];
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public decimal LoyaltyCredit { get; set; }

        [InverseProperty("Customer")]
        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; } = [];
    }
}