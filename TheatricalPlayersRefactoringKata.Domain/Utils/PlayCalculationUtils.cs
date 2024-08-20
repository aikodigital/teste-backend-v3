using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Utils {

    public static class PlayCalculationUtils {
        public static double CalculatePlayLines(Performance perf, Play play) {

            int lines = play.Lines;
            lines = lines < 1000 ? 1000 : lines > 4000 ? 4000 : lines;

            double thisAmount = lines / 10; // revisar se é divisão ou multiplicação

            return thisAmount;
        }
    }
}
