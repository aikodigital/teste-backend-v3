using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TS.Domain.Entities
{
    public class InvoicesItems
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long InvoiceId { get; set; }
        public long PerformanceId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public virtual Invoice Invoice { get; set; } = new();
        [ForeignKey(nameof(PerformanceId))]
        public virtual Performance Performance { get; set; } = new();
    }
}