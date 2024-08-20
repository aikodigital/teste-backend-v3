using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Services
{
    public static class Constants
    {
        public const int MIN_LINES = 1000;
        public const int MAX_LINES = 4000;
        public const int BASE_COST_PER_LINE = 10;
        public const int TRAGEDY_EXTRA_COST_PER_AUDIENCE = 1000;
        public const int COMEDY_BASE_COST = 10000;
        public const int COMEDY_EXTRA_COST_PER_AUDIENCE = 500;
        public const int COMEDY_ADDITIONAL_COST_PER_AUDIENCE = 300;
        public const int COMEDY_BONUS_THRESHOLD = 20;
        public const int CREDIT_THRESHOLD = 30;
        public const int COMEDY_BONUS_FACTOR = 5;
    }
}
