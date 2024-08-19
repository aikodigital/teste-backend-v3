using System;

namespace TheatricalPlayersRefactoringKata.Calculo_Credito;

public class CalculoCreditoEspectador : ICalculoCreditoEspectador
{
    private readonly int numeroMinimoDePessoasParaReceberCreditos = 30;
    private readonly int bonusCreditoUmQuintoDaPlateia = 5;

    public int CalculaCredito(Performance perf, Play play)
    {
        var volumeCredits = 0;

        volumeCredits += Math.Max(perf.Audience - numeroMinimoDePessoasParaReceberCreditos, 0);

        if (play.Type == "comedy")
            volumeCredits += (int)Math.Floor((decimal)perf.Audience / bonusCreditoUmQuintoDaPlateia);


        return volumeCredits;
    }
}
