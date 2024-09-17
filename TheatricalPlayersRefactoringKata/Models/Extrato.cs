using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Extrato
    {
        [Key, Display(Name = "Id")]
        public int Id { get; set; }

        [Required, StringLength(50), Display(Name = "Nome")]
        public string PlayName { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true), Display(Name = "Valor")]
        public decimal AmountOwed { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true), Display(Name = "Creditos")]
        public int EarnedCredits { get; set; }

        [Required, Display(Name = "Assentos")]
        public int Seats { get; set; }
    }
}
