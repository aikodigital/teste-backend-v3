using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

public class StatementProcessor
{
    private readonly Queue<Invoice> _invoiceQueue = new Queue<Invoice>();
    private readonly XmlStatementPrinter _xmlStatementPrinter = new XmlStatementPrinter();

    // Adiciona uma fatura à fila
    public void EnqueueInvoice(Invoice invoice)
    {
        _invoiceQueue.Enqueue(invoice);
    }

    // Processa as faturas na fila assincronamente
    public async Task ProcessInvoicesAsync(Dictionary<string, Play> plays, string outputDirectory)
    {
        while (_invoiceQueue.Count > 0)
        {
            var invoice = _invoiceQueue.Dequeue();
            var xmlDocument = _xmlStatementPrinter.Print(invoice, plays);
            var filePath = Path.Combine(outputDirectory, $"{invoice.Customer}-statement.xml");

            using (var writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(xmlDocument.ToString());
            }
        }
    }
}
