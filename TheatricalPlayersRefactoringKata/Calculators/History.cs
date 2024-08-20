using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class History : IPlay
    {
        private readonly IPlay _tragedyCalculator = new Tragedy();
        private readonly IPlay _comedyCalculator = new Comedy();

        public double CalculateAmount(Play play, Performance performance)
        {
            var tragedyAmount = _tragedyCalculator.CalculateAmount(play, performance);
            var comedyAmount = _comedyCalculator.CalculateAmount(play, performance);
            return tragedyAmount + comedyAmount;
        }

        public int CalculateVolumeCredits(Performance performance)
        {
            // Credits for history are same as for tragedy
            return _tragedyCalculator.CalculateVolumeCredits(performance);
        }
    }
}
