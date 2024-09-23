using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.enums;

namespace TheatricalPlayersRefactoringKata;

public class PrintStatement : IPrintStatementBase
{   
    public string Print(Statement statement,PrintType printType)
    {
        string result= string.Empty;
        var test =  printType.ToString();
        switch (printType)
        {
            case PrintType.text:
                result = PrintToText(statement);
                break;

            case PrintType.xml:
                result = PrintToXML(statement);
                break;

            default:
                throw new Exception("Unknowm type of file: " + printType.GetType().Name.ToString());
        }

        return result;
    }

    public string PrintToXML(Statement statement)
    {
       
         var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapping()))
         .CreateMapper();
 
         var statementToXml = mapper.Map<StatementToXml>(statement);

        return Services.Serialize.SerializeStatement.SerializeToXMl(statementToXml);
    }
 
    public string PrintToText(Statement statement)
    {
        
        CultureInfo cultureInfo = new CultureInfo("en-US");
        StringBuilder result = new StringBuilder();
        result.AppendFormat("Statement for {0}\n", statement.Customer);
        foreach(var item in statement.Items) 
        {
            result.AppendFormat(cultureInfo,"  {0}: {1:C} ({2} seats)\n", item.Name, item.AmountOwed, item.Seats);
        }
        result.AppendFormat(String.Format(cultureInfo, "Amount owed is {0:C}\n", statement.AmountOwed));
        result.AppendFormat("You earned {0} credits\n", statement.EarnedCredits);
        return result.ToString();
    }

}


