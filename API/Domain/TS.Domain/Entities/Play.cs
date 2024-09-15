using System.ComponentModel.DataAnnotations;

namespace TS.Domain.Entities
{
    public class Play
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Type { get; set; }
        [Required]
        public int Lines { get; set; }
    }
}