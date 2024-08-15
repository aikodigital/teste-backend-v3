using System;

namespace TheatricalPlayersRefactoringKata.Calculo_Credito;

public static class CalculoCreditoEspectador
{
    public static int CalculaCredito(Invoice invoice, Play play)
    {
        int numeroMinimoDePessoasParaReceberCreditos = 30;
        var volumeCredits = 0;

        foreach (var perf in invoice.Performances)
        {
            volumeCredits += Math.Max(perf.Audience - numeroMinimoDePessoasParaReceberCreditos, 0);

            if (play.Type == "comedy")
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
        }

        return volumeCredits;
    }
}
