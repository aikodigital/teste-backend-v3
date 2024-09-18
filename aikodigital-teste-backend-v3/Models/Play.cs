using System.ComponentModel.DataAnnotations;

namespace aikodigital_teste_backend_v3.Models
{
    public class Play
    {
        [Key]
        public int Id { get; set; } 
        public required string Name { get; set; }
        public required string Type { get; set; }
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
