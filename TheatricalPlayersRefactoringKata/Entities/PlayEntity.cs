using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    [Table("Play")]
    public class PlayEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Lines { get; set; }
        public string Type { get; set; }
    }
}

