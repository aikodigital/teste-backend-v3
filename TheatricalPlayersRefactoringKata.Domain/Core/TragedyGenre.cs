
using System;
using TheatricalPlayersRefactoringKata;

public class TragedyGenre : IPlayGenre {

    public int CalculatePlayCredits(Performance perf) {

        int volumeCredits = Math.Max(perf.Audience - 30, 0);

        // add extra credit for every ten comedy attendees
        volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

        return volumeCredits;
    }

    public double CalculatePlayAmount(Performance perf) {

        double thisAmount = 0;

        if (perf.Audience > 30) {
            thisAmount += 1000 * (perf.Audience - 30);
        }


        return thisAmount;
    }
}