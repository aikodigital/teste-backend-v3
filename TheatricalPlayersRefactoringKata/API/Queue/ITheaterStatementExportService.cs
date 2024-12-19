using System.IO;
using System.Text;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.API.DTOs;
using TheatricalPlayersRefactoringKata.API.Queue;

public class IStatementExportService : ITheaterStatementExportService
{
    public string GerarExtratoXml(TheaterInvoiceResponseDTO invoiceDto)
    {
        var stringBuilder = new StringBuilder();
        var serializer = new XmlSerializer(typeof(TheaterInvoiceResponseDTO));

        using (var writer = new StringWriter(stringBuilder))
        {
            serializer.Serialize(writer, invoiceDto);
        }

        return stringBuilder.ToString();
    }
}
