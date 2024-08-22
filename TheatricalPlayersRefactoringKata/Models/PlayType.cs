using System;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class PlayType
    {
        [Key]
        public string Name { get; set; }
        public DateTime DtInclusao { get; set; }
        public string? Description { get; set; }
    }
}
