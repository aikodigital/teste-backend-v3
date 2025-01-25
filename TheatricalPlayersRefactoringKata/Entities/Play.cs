using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Play
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Lines { get; set; }
        public string Type { get; set; }
    }
}

