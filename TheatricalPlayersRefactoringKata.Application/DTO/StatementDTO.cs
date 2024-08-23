using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.DTO
{
    public class StatementDTO
    {
        public string CustomerName { get; set; }
        public PlayDTO PlayDTO { get; set; }
        public double AmountOwed { get; set; }
        public double EarnedCredits { get; set; }
        public int PlayId { get; set; }

        public StatementDTO(string customerName, PlayDTO playDTO, double amountOwed, double earnedCredits, int playId)
        {
            CustomerName = customerName;
            PlayDTO = playDTO;
            AmountOwed = amountOwed;
            EarnedCredits = earnedCredits;
            PlayId = playId;
        }
    }
}
