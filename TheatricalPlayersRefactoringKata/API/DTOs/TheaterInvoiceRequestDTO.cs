using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.DTOs
{
    public class TheaterInvoiceRequestDTO
    {
        public string Customer { get; set; } = string.Empty;
        public List<Performance> Performances { get; set; } = new List<Performance>(); 
    }
}
