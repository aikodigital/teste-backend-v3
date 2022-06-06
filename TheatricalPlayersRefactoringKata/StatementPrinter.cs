using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly CultureInfo _cultureInfo;
    
    public StatementPrinter()
    {
        _cultureInfo = new CultureInfo("en-US");
    }

    public string Print(StatementDto statementDto)
    {
        var result = string.Format("Statement for {0}\n", statementDto.Customer);

        statementDto.Items.ToList().ForEach(i => 
            result += String.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", i.Name, i.AmountOwed, i.Seats)
        );

        result += String.Format(_cultureInfo, "Amount owed is {0:C}\n", statementDto.AmountOwed);
        result += String.Format("You earned {0} credits\n", statementDto.EarnedCredits);
        
        return result;
    }

    public string PrintXml(StatementDto statementDto)
    {
        using var memoryStream = new MemoryStream();
        using var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings { Indent = true });
        
        var serializer = new XmlSerializer(typeof(StatementDto));
        serializer.Serialize(xmlWriter, statementDto);

        xmlWriter.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);

        using (var sr = new StreamReader(memoryStream, Encoding.Unicode))
        {
            return sr.ReadToEnd();
        }
    }
}
