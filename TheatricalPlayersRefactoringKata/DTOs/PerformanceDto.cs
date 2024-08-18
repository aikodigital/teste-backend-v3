using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.DTOs
{
    public class PerformanceDto
    {
        [Required]
        public string PlayName { get; set; }

        [Required]
        public int Audience { get; set; }
    }
}
