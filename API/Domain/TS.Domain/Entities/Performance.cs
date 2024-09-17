using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TS.Domain.Entities
{
    public class Performance
    {
        public Performance()
        {
            InvoicesItems = [];
        }

        [Key]
        public long Id { get; set; }
        [Required]
        public long PlayId { get; set; }
        [Required]
        public int Audience { get; set; }

        [ForeignKey(nameof(PlayId))]
        public virtual Play Play { get; set; } = new();

        [InverseProperty("Performance")]
        [JsonIgnore]
        public virtual ICollection<InvoicesItems> InvoicesItems { get; set; } = [];
    }
}