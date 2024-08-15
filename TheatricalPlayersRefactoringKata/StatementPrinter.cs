using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculo_Credito;
using TheatricalPlayersRefactoringKata.Factory;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly IPlayTypeFactory _factory;

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];

            var calculoEmRelacaoAoTipo = _factory.FabricaDeTypes(play);
            
            totalAmount += calculoEmRelacaoAoTipo.CalculaValoresBase(invoice, play);

            volumeCredits += CalculoCreditoEspectador.CalculaCredito(invoice, play);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(totalAmount / 100), perf.Audience);
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
