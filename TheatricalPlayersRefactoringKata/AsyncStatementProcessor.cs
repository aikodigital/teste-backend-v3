using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class AsyncStatementProcessor
    {
        public async Task GenerateXmlAsync(Invoice invoice, Dictionary<string, Play> plays, string filePath)
        {
            var xmlPrinter = new XmlStatementPrinter();
            var xmlContent = xmlPrinter.Print(invoice, plays);
            await File.WriteAllTextAsync(filePath, xmlContent);
        }
    }
}
