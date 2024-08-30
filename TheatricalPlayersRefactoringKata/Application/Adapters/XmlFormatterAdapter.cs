using TheatricalPlayersRefactoringKata.Models.Dtos;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Entities;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;

namespace TheatricalPlayersRefactoringKata.Application.Adapters;
public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => new UTF8Encoding(true);
}
public class XmlFormatterAdapter : IFormatterAdapter
{
    public string Format(Statement statement)
    {
        // TODO: AJUSTAR CRIAÇÃO DO DTO
        var statementDto = new StatementDto
        {
            Customer = statement.Customer,
            TotalAmountOwed = statement.TotalAmountOwed,
            TotalEarnedCredits = statement.TotalEarnedCredits,
            Items = statement.Items.Select(item => new StatementItemDto
            {
                AmountOwed = item.AmountOwed,
                EarnedCredits = item.EarnedCredits,
                Seats = item.Seats
            }).ToList()
        };

        var xmlSerializer = new XmlSerializer(typeof(StatementDto));
        var settings = new XmlWriterSettings
        {
            Indent = true,
        };

        using (var stringWriter = new Utf8StringWriter())
        using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
        {
            xmlSerializer.Serialize(xmlWriter, statementDto);
            return stringWriter.ToString();
        }
    }
}
