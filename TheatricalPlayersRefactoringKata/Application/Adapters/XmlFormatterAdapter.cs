using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Entities;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;
using AutoMapper;
using TheatricalPlayersRefactoringKata.Application.Models.Dtos;

namespace TheatricalPlayersRefactoringKata.Application.Adapters;

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => new UTF8Encoding(true);
}

public class XmlFormatterAdapter : IFormatterAdapter
{
    private readonly IMapper _mapper;

    public XmlFormatterAdapter(IMapper mapper) 
    {
        _mapper = mapper;
    }

    public string Format(Statement statement)
    {
        var statementDto = _mapper.Map<StatementDto>(statement);

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
