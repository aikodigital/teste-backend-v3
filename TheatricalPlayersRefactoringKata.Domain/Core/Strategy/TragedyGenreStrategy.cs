
using System;
using TheatricalPlayersRefactoringKata;

public class TragedyGenreStrategy : IGenreStrategy {

    public int CalculatePlayCredits(Performance perf) {
        int volumeCredits = Math.Max(perf.Audience - 30, 0); 

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