using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.DTO
{
    public class PlayDTO
    {
        public string PlayName { get; set; }
        public string AmountOwed { get; set; }
        public string EarnedCredits { get; set; }
        public string Seats { get; set; }

        public PlayDTO(string playName, string amountOwed, string earnedCredits, string seats)
        {
            PlayName = playName;
            AmountOwed = amountOwed;
            EarnedCredits = earnedCredits;
            Seats = seats;
        }
    }
}
