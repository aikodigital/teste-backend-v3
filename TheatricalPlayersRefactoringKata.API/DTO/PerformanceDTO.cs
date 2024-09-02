using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.API.DTO
{
    public class PerformanceDTO
    {
        public string PlayId { get; set; }
        public int Audience { get; set; }
    }
}
