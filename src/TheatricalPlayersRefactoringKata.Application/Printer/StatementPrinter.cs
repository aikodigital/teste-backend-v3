using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Printer
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays, string fileType)
        {
            return fileType switch
            {
                "txt" => new TxtStatementPrinter().Print(invoice, plays),
                "xml" => new XmlStatementPrinter().Print(invoice, plays),
                _ => throw new Exception("unknown type: " + fileType)
            };
        }
    }
}