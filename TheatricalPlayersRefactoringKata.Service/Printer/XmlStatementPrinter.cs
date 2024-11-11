using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Model;
using TheatricalPlayersRefactoringKata.Service.Calculators;

namespace TheatricalPlayersRefactoringKata.Service.Printer;

public class XmlStatementPrinter : IStatementPrinter
{    
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        CalculateInvoice(invoice, plays);

        string xmlString = BuildXmlString(invoice);

        return xmlString;
    }

    public async Task PrintAndExport(Invoice invoice, Dictionary<string, Play> plays, string filePath)
    {
        var xml = Print(invoice, plays);

        await ExportXml(xml, filePath);
    }

    private static async Task ExportXml(string xml, string filePath)
    {
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
        
        var settings = new XmlWriterSettings
        {
            Async = true,
            Indent = true
        };

        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        using var writer = XmlWriter.Create(fileStream, settings);

        var xmlDoc = new XmlDocument();
        xmlDoc.Load(memoryStream); 

        await Task.Run(() => xmlDoc.WriteTo(writer));
    }

    private string BuildXmlString(Invoice invoice)
    {
        try
        {
            Encoding utf8 = Encoding.UTF8;

            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter writer = new StreamWriter(memoryStream, utf8);

            XmlSerializer serializer = new XmlSerializer(typeof(Invoice));
            serializer.Serialize(writer, invoice);

            return utf8.GetString(memoryStream.ToArray());
        }
        catch (Exception ex) when (ex is ArgumentNullException || ex is IOException || ex is InvalidOperationException)
        {
            string errorMessage = $"Error: Exception of type {ex.GetType().Name} occurred. Message: {ex.Message}";
            errorMessage += $"\nStackTrace: {ex.StackTrace}";
           Console.Write(errorMessage);

            return string.Empty;
        }
    }

    private static void CalculateInvoice(Invoice invoice, Dictionary<string, Play> plays)
    {
        List<Performance> performances = invoice.Performances;

        foreach (var perf in performances) 
        {
            var play = plays[perf.PlayId];
            
            StatementCalculator statementCalculator = GetStatementCalculatorGenre(play.Genre);
            var amountOwed = statementCalculator.CalculateAmountOwned(perf, play);
            var earnedCredits = statementCalculator.CalculateCredits(perf.Audience);
            
            perf.AmountOwed = Convert.ToDecimal(amountOwed) / 100;
            perf.EarnedCredits = earnedCredits;
            
            invoice.AmountOwed += perf.AmountOwed;
            invoice.EarnedCredits += perf.EarnedCredits;
        }
    }

    private static StatementCalculator GetStatementCalculatorGenre(Genre genre)
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
}
