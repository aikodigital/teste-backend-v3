using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statment;

public class XmlStatementFormatter : IStatementFormatter
{
    private readonly IInvoiceCalculationService _invoiceCalculationService;
    private readonly Dictionary<string, IPerformanceCalculator> _calculators;

    public XmlStatementFormatter(IInvoiceCalculationService invoiceCalculationService, Dictionary<string, IPerformanceCalculator> calculators)
    {
        _invoiceCalculationService = invoiceCalculationService;
        _calculators = calculators;
    }

    public async Task<string> FormatAsync(Invoice invoice)
    {
        var result = new StringBuilder();
        var serializer = new XmlSerializer(typeof(Invoice));

        using (var writer = new StringWriter(result))
        {
            serializer.Serialize(writer, invoice);
        }

        return result.ToString();
    }
}