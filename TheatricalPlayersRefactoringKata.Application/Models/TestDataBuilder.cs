using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.Models
{
    public class TestDataBuilder
    {
        public static Play CreatePlay(string name, int lines, string type) =>
        new Play(name, lines, type);

        public static Performance CreatePerformance(string playId, string playName, int audience, int lines, string type) =>
            new Performance(playId, audience) { Play = CreatePlay(playName, lines, type) };

        public static IEnumerable<object[]> TragedyTestCases => new List<object[]>
        {
            new object[] { 30, 300.0 }, // Base value only
            new object[] { 35, 350.0 } // Base + (5 * 10.0)
        };

        public static IEnumerable<object[]> ComedyTestCases => new List<object[]>
        {
            new object[] { 15, 345.0 }, // Base + (15 * 3.00)
            new object[] { 25, 500.0 } // Base + (25 * 3.00) + 100.00 + (5 * 5.00)
        };

        public static IEnumerable<object[]> CreditsBasedOnAudienceTest => new List<object[]>
        {
            new object[] { "tragedy", 30, 0 }, // No credits for audience <= 30
            new object[] { "tragedy", 40, 10 }, // 10 audience over 30
            new object[] { "comedy", 30, 6 }, // Comedy, audience = 30, 6 credit bonus over audience (1/5)
            new object[] { "comedy", 40, 18 }, // 8 credits (1/5)
            new object[] { "history", 30, 0 }, // No credits for audience <= 30
            new object[] { "history", 40, 10 } // 10 audience over 30
        };

        public static IEnumerable<object[]> BaseValueBasedOnLinesTest => new List<object[]>
        {
            new object[] { 500, 100.0 }, // Limite inferior ajustado para 1000
            new object[] { 3500, 350.0 }, // Dentro do intervalo
            new object[] { 4500, 400.0 }, // Limite superior ajustado para 4000
            new object[] { 0, 100.0 }, // Min value for below 1000 lines
            new object[] { 5000, 400.0 }, // Max value for above 4000 lines
        };

        public static IEnumerable<object[]> HistoricalPlayTest => new List<object[]>
        {
            new object[] { 25, 800.0 }, // Tragedy: 300 + (0 * 10) = 300 Comedy: 300 + (25 * 3) + 100 + (5 * 5) = 500
            new object[] { 50, 1200.0 }, // Tragedy: 300 + (20 * 10) = 500 Comedy: 300 + (50 * 3) + 100 + (30 * 5) = 700
            new object[] { 100, 2100.0 } // Tragedy: 300 + (70 * 10) = 1000 Comedy: 300 + (100 * 3) + 100 + (80 * 5) = 1100
        };

        public static IEnumerable<object[]> ComedyCostBasedOnAudienceTest => new List<object[]>
        {
            new object[] { 15, 345.0 }, // Base + (15 * 3.00)
            new object[] { 25, 500.0 } // Base + (25 * 3.00) + 100.00 + (5 * 5.00)
            
        };

        public static IEnumerable<object[]> TragedyCostBasedOnAudienceTest => new List<object[]>
        {
            new object[] { 30, 300.0 }, // Base value only
            new object[] { 35, 350.0 } // Base + (5 * 10.0)

        };
    }
}
