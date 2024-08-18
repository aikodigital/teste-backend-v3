using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 2000, "tragedy") },
                { "as-like", new Play("As You Like It", 1500, "comedy") },
                { "othello", new Play("Othello", 3000, "tragedy") },
                { "history-play", new Play("Historical Play", 2000, "historical") }
            };

            var performances = new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("history-play", 25)
            };

            var invoice = new Invoice("John Doe", performances);
            var printer = new StatementPrinter();

            var result = printer.Print(invoice, plays);

            Console.WriteLine(result);
        }
    }
}
