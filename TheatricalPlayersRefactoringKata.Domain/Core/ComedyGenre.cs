using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Core {
    public class ComedyGenre : IPlayGenre {

        public int CalculatePlayCredits(Performance perf) {
            return 0;
        }

        public double CalculatePlayAmount(Performance perf) {
            return 0;
        }
    }
}
