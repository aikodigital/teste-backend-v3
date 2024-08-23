using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Domain.Common.Result;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Utils;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories {
    public class StatementPrinterRepositoryImpl : IStatementPrinterRepository {


        // adicionar context do banco de dados para persistência dos dados no banco

        public Result<string> PrintText(Invoice invoice, Dictionary<string, Play> plays, Dictionary<Enum, IGenreStrategy> genres) {
            CultureInfo cultureInfo = new("en-US");

            if (invoice == null) {
                return Result<string>.Failure(Error.Validation("Invoice data is null", ErrorType.Validation.ToString()));
            }

            if (plays == null && plays!.Count == 0) {
                return Result<string>.Failure(Error.Validation("Plays data is null", ErrorType.Validation.ToString()));
            }

            double totalAmount = 0;
            double totalVolumeCredits = 0;
            string result = string.Format("Statement for {0}\n", invoice.Customer);

            foreach (var perf in invoice.Performances) {
                double unitVolumeCredits = 0;

                Play play = plays[perf.PlayId];
                double thisAmount = 0;
                (thisAmount, totalAmount, unitVolumeCredits) = CalculateAllPerfomances(plays, play, genres, perf, unitVolumeCredits, totalAmount);

                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalVolumeCredits += unitVolumeCredits;
            }

            string xmlString = PrintXml(invoice, plays, genres);

            string filePath = Path.Combine(Path.GetTempPath(), "invoice.xml");
            File.WriteAllText(filePath, xmlString);

            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += string.Format("You earned {0} credits\n", totalVolumeCredits);
            return Result<string>.Success(result);
            ;
        }

        public string PrintXml(Invoice invoice, Dictionary<string, Play> plays, Dictionary<Enum, IGenreStrategy> genres) {
            double totalAmount = 0;
            double totalVolumeCredits = 0;
            XDocument xmlDocument = new();
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XElement statementEl = new(new XElement("Statement", new XAttribute(XNamespace.Xmlns + "xsi", xsi), new XAttribute(XNamespace.Xmlns + "xsd", xsd)));

            XElement customerEl = new( new XElement("Customer"));
            customerEl.Value = invoice.Customer;
            statementEl.Add(customerEl);

            XElement itemsEl = new(new XElement("Items"));
            foreach (var perf in invoice.Performances) {
                double unitVolumeCredits = 0;
                double thisAmount = 0;
                Play play = plays[perf.PlayId];
                (thisAmount, totalAmount, unitVolumeCredits) = CalculateAllPerfomances(plays, play, genres, perf, unitVolumeCredits, totalAmount);

                XElement itemEl = new(new XElement("Item"));
                XElement amountEl = new(new XElement("AmountOwed", Convert.ToDecimal(thisAmount / 100)));
                XElement creditsEl = new(new XElement("EarnedCredits", unitVolumeCredits));
                XElement seatsEl = new(new XElement("Seats", perf.Audience.ToString()));

                itemEl.Add(amountEl);
                itemEl.Add(creditsEl);
                itemEl.Add(seatsEl);

                itemsEl.Add(itemEl);
                totalVolumeCredits += unitVolumeCredits;
            }
            if (itemsEl.HasElements) {
                statementEl.Add(itemsEl);
            }

            XElement totalAmountEl = new(new XElement("AmountOwed", Convert.ToDecimal(totalAmount / 100)));
            XElement totalCredits = new(new XElement("EarnedCredits", totalVolumeCredits));

            statementEl.Add(totalAmountEl);
            statementEl.Add(totalCredits);

            xmlDocument.Add(statementEl);
            return IndentXml(xmlDocument);
        }

        private static (double, double, double) CalculateAllPerfomances(Dictionary<string, Play> plays, Play play, Dictionary<Enum, IGenreStrategy> genres, Performance perf,  double volumeCredits, double totalAmount) {
            double thisAmount = PlayCalculationUtils.CalculatePlayLines(perf, play);

            if (!plays.TryGetValue(perf.PlayId, out play!)) {
                Result<string>.Failure(Error.NotFound($"Play with ID {perf.PlayId} not found.", ErrorType.NotFound.ToString()));
            }

            if (!genres.TryGetValue(play.Type, out var genre)) {
                Result<string>.Failure(Error.NotFound($"Genre strategy for {play.Type} not found.", ErrorType.NotFound.ToString()));
            }

            thisAmount = genre!.CalculatePlayAmount(perf, thisAmount);
            volumeCredits += genre.CalculatePlayCredits(perf);
            totalAmount += thisAmount;

            return (thisAmount, totalAmount, volumeCredits);
        }

        private string IndentXml(XDocument doc) {
            string xmlString = "";
            XmlWriterSettings settings = new XmlWriterSettings {
                Encoding = new UTF8Encoding(false), 
                Indent = true,
                OmitXmlDeclaration = false 
            };

            using (Utf8StringWriter utf8Sw = new()) {
                using (XmlWriter writer = XmlWriter.Create(utf8Sw, settings)) {
                    doc.Save(writer);
                }
                xmlString = utf8Sw.ToString();
            }

            return xmlString;
        }
    }
}
