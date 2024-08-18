using System;
using TheatricalPlayersRefactoringKata.Enum;

namespace TheatricalPlayersRefactoringKata.Calculo_Credito;

public class CreditoEspectador : ICreditosEspectador
{
    private readonly int minCredito = 30;
    private readonly int bonusUmQuinto = 5;

    public int CalculoCredito(PerformanceModel perf, PlayModel play)
    {
        var volumeCredits = 0;

        volumeCredits += Math.Max(perf.Audience - minCredito, 0);

        if (play.Type == "comedy") volumeCredits += (int)Math.Floor((decimal)perf.Audience / bonusUmQuinto);

        return volumeCredits;
    }
}