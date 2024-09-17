using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.ViewModels
{
    public class DetalhesDaApresentacaoViewModel
    {        
        public string PlayName { get; set; }        
        public decimal AmountOwed { get; set; }        
        public int EarnedCredits { get; set; }        
        public int Seats { get; set; }
    }
}
