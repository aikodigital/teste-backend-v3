using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculo_Credito;
using TheatricalPlayersRefactoringKata.Factory;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly IPlayTypeFactory _factory;
    private readonly ICalculoCreditoEspectador _creditoEspectador;

    public StatementPrinter(IPlayTypeFactory factory, ICalculoCreditoEspectador creditoEspectador)
    {
        _factory = factory;
        _creditoEspectador = creditoEspectador;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var thisAmount = 0;

            var calculoEmRelacaoAoTipo = _factory.FabricaDeTypes(play);

            thisAmount += calculoEmRelacaoAoTipo.CalculaValoresBase(perf, play);

            volumeCredits += _creditoEspectador.CalculaCredito(perf, play);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, 
                      Convert.ToDecimal(thisAmount), perf.Audience);

            totalAmount += calculoEmRelacaoAoTipo.CalculaValoresBase(perf, play);
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
