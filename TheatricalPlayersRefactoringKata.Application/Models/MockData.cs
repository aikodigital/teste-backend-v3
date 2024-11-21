using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.Models
{
    public class MockData
    {
        public static Dictionary<string, Play> GetPlays()
        {
            return new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") }
        };
        }

        public static Dictionary<string, Play> GetPlays1()
        {
            return new Dictionary<string, Play>
    {
        { "hamlet", new Play("Hamlet", 4024, "tragedy") },
        { "as-like", new Play("As You Like It", 2670, "comedy") },
        { "othello", new Play("Othello", 3560, "tragedy") },
        { "henry-v", new Play("Henry V", 3227, "history") },
        { "john", new Play("King John", 2648, "history") },
        { "richard-iii", new Play("Richard III", 3718, "history") }
    };
        }

        public static Invoice GetInvoice1()
        {
            return new Invoice(
                "BigCo",
                new List<Performance>
                {
            new Performance("hamlet", 55),
            new Performance("as-like", 35),
            new Performance("othello", 40),
            new Performance("henry-v", 20),
            new Performance("john", 39),
            new Performance("henry-v", 20)
                }
            );
        }

        public static Invoice GetInvoice()
        {
            return new Invoice(
                "BigCo",
                new List<Performance>
                {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                }
            );
        }
    }
}
