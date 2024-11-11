using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Model;
using TheatricalPlayersRefactoringKata.Service.Calculators;

namespace TheatricalPlayersRefactoringKata.Service.Printer;

public class StatementPrinter : IStatementPrinter
{
    private readonly CultureInfo _cultureInfo = new CultureInfo("en-US");
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var printString = BuildResultString(invoice, plays);
        return printString;
    }

    private string BuildResultString(Invoice invoice, Dictionary<string, Play> plays)
    {
        string lineOrder = string.Empty;
        uint totalAmount = 0;
        uint volumeCredits = 0;

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            
            StatementCalculator calculator = GettStatementCalculatorGenre(play.Genre);

            var thisAmount = calculator.CalculateAmountOwned(perf, play);
            volumeCredits += calculator.CalculateCredits(perf.Audience);
            
            lineOrder += string.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount) / 100, perf.Audience);

            totalAmount += thisAmount;
        }

        return ResultConcat(invoice.Customer, lineOrder, totalAmount, volumeCredits);
    }

    private static StatementCalculator GettStatementCalculatorGenre(Genre genre)
    {
        switch (genre) 
        {
            case Genre.tragedy:
                return new StatementCalculator(new TragedyGenreCalculator());
            case Genre.comedy:
                return new StatementCalculator(new ComedyGenreCalculator());
            case Genre.history:
                return new StatementCalculator(new HistoryGenreCalculator());
            default:
                throw new Exception("unknown type: " + genre.ToString());
        }
    }

    private string ResultConcat(string customer, string lineOrder, uint totalAmount, uint volumeCredits)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(string.Format("Statement for {0}\n", customer))
        .Append(lineOrder)
        .Append(string.Format(_cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount) / 100))
        .Append(string.Format("You earned {0} credits\n", volumeCredits));
        
        return sb.ToString();
    }
}
