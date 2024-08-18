using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    public class HistoryCharge : IChargeStrategy
    {
        public int CalculateBilling(Performance performance, Play play)
        {
            var tragedyStrategy = new TragedyCharge();
            var comedyStrategy = new CollectionChargy();

            var tragedyAmount = tragedyStrategy.CalculateBilling(performance, play);
            var comedyAmount = comedyStrategy.CalculateBilling(performance, play);

            return tragedyAmount + comedyAmount;
        }

        public int CalculateCredits(Performance performance)
        {
            var tragedyStrategy = new TragedyCharge();
            var comedyStrategy = new CollectionChargy();

            var tragedyCredits = tragedyStrategy.CalculateCredits(performance);
            var comedyCredits = comedyStrategy.CalculateCredits(performance);

            return tragedyCredits + comedyCredits;
        }
    }
}
