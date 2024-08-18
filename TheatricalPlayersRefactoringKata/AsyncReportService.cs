using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata;

public class AsyncReportService
{
    private readonly StatementPrinter _statementPrinter;

    public AsyncReportService()
    {
        _statementPrinter = new StatementPrinter();
    }

    public async Task<string> GenerateAndSave(Invoice invoice, Dictionary<string, Play> plays, string outputDirectory)
    {
        // Gera o conteúdo XML
        var xmlContent = _statementPrinter.PrintXml(invoice, plays);

        // Converte o XDocument para string
        var xmlString = xmlContent.ToString();

        // Define o caminho do arquivo
        var filePath = Path.Combine(outputDirectory, "report.xml");

        // Salva o XML como string no arquivo
        await File.WriteAllTextAsync(filePath, xmlString);

        return filePath;
    }
}
