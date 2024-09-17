using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TS.Domain.Entities
{
    public class Play
    {
        public Play()
        {
            Performances = [];
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Type { get; set; }
        [Required]
        public int Lines { get; set; }

        [InverseProperty("Play")]
        [JsonIgnore]
        public virtual ICollection<Performance> Performances { get; set; } = [];
    }
}