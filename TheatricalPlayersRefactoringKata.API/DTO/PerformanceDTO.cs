using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.API.DTO
{
    public class PerformanceDTO
    {
        [Required]
        public string PlayId { get; set; }

        [Required]
        public int Audience { get; set; }
    }
}
