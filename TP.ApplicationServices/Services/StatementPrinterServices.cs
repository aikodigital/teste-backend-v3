using System.Globalization;
using TP.ApplicationServices.Interfaces;
using TP.Domain.Entities;

public class StatementPrinterServices : IStatementPrinterServices
{
    private readonly CultureInfo _usCulture = new CultureInfo("en-US");
    private readonly StatementCalculator _calculator = new StatementCalculator();

    public string Print(Invoice invoice, Dictionary<string, Play> plays, string format)
    {
     /*   var invoice = _invoiceRepository.GetInvoiceById(invoiceId); // Recupera a fatura do banco de dados
        if (invoice == null) throw new Exception("Invoice not found");

        var plays = _playRepository.GetAllPlays().ToDictionary(p => p.Name, p => p); // Mapeia plays pelo ID*/

        IStatementFormatter formatter = format switch
        {
            "xml" => new XmlStatementFormatter(),
            _ => new TextStatementFormatter(),
        };

        var totalAmount = 0m;
        var volumeCredits = 0;

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var (thisAmount, credits) = _calculator.CalculateAmountAndCredits(play, perf);

            volumeCredits += credits;
            totalAmount += thisAmount;
        }

        return formatter.FormatStatement(invoice, plays, totalAmount, volumeCredits);
    }
}
