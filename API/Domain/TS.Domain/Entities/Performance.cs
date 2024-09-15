using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TS.Domain.Entities
{
    public class Performance
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long PlayId { get; set; }
        [Required]
        public int Audience { get; set; }

        [ForeignKey(nameof(PlayId))]
        public virtual Play Play { get; set; } = new();
    }
}