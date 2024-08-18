using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Factory;

namespace TheatricalPlayersRefactoringKata;
public class StatementPrinter{
    private readonly IFazerTipos _factory;
    private readonly ICreditosEspectador _creditoEspectador;

    public StatementPrinter(IFazerTipos factory, ICreditosEspectador creditoEspectador)
    {
        _factory = factory;
        _creditoEspectador = creditoEspectador;
    }

    public string Print(InvoiceModel invoice, Dictionary<string, PlayModel> plays)
    {
        decimal totalAmount = 0;
    var volumeCredits = 0;
    var result = $"Statement for {invoice.Customer}\n";
    CultureInfo cultureInfo = new CultureInfo("en-US");

    foreach (var perf in invoice.Performances)
    {
        var play = plays[perf.PlayId];
        decimal thisAmount = 0;

        var calculoEmRelacaoAoTipo = _factory.FabricaDeTypes(play);
        thisAmount += calculoEmRelacaoAoTipo.CalculoBase(perf, play);

        volumeCredits += _creditoEspectador.CalculoCredito(perf, play);

        result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);

        totalAmount += thisAmount;
    }

    result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount));
    result += $"You earned {volumeCredits} credits\n";
    return result;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}