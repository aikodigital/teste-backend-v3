using System;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Play
    {
        [Key]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public int Lines { get; set; }

        public Play()
        {
        }

        public Play(string name, int lines, string type)
        {
            Name = name;
            Lines = lines;
            Type = type;
        }
    }
}
