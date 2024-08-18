using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.DTOs
{
    public class PerformanceDto
    {
        [Required]
        public string PlayId { get; set; }

        [Required]
        public int Audience { get; set; }
    }
}
