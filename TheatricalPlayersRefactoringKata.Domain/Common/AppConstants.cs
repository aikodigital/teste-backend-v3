using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Common {
    public static class AppConstants {

        public static CultureInfo cultureInfo = new("en-US");

        public static double CalculatePlayLines(Performance perf, Play play) {

            int lines = play.Lines;
            lines = lines < 1000 ? 1000 : lines > 4000 ? lines : lines;

            double thisAmount = lines / 10; // revisar se é divisão ou multiplicação

            return thisAmount;
        }
    }
}
