using System;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Formatters.Interface;
using TheatricalPlayersRefactoringKata.Helpers;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Models.Xml;

namespace TheatricalPlayersRefactoringKata.Formatters;

public class XmlStatementFormatter : IExtratoFormatter
{
    public string FormatCustomer(string customer)
    {
        return customer;
    }

    public string FormatPerformance(Play play, Performance performance, decimal performanceAmount, decimal performaceCredits, CultureInfo cultureInfo)
    {
        return $"{(performanceAmount / 100).ToString("0.#", cultureInfo)},{performaceCredits},{performance.Audience}\n";

    }

    public string FormatTotalAmount(decimal totalAmount, CultureInfo cultureInfo)
    {
        return totalAmount.ToString();
    }

    public string FormatTotalCredits(int totalCredits)
    {
        return totalCredits.ToString();
    }

    public string GenerateStatement(string customer, string performace, string totalAmount, string totalCredits, CultureInfo cultureInfo)
    {
        var linha = performace.Split("\n");
        var itens = new ItemsXml();
        foreach (var valores in linha)
        {
            if (valores != null && valores != string.Empty)
            {
                var item = valores.Split(",");
                itens.ItemList.Add(new Item { AmountOwed = item[0], EarnedCredits = item[1], Seats = item[2] });
            }           
        }

        var totalAmountDecimal = decimal.Parse(totalAmount);
        totalAmountDecimal = totalAmountDecimal / 100;
        string totalAmountConvertido = totalAmountDecimal.ToString("0.#", cultureInfo);

        Statement statement = new Statement
        {
            Customer = customer,
            Items = itens,
            AmountOwed = totalAmountConvertido,
            EarnedCredits = totalCredits
        };


        XmlWriterSettings settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(false),
            Indent = true
        };

        XmlSerializer serializer = new XmlSerializer(typeof(Statement));
        string xmlString;

        using (var stringWriter = new Utf8StringWriter())
        using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
        {
            serializer.Serialize(xmlWriter, statement);
            xmlWriter.Flush();

            string bom = Encoding.UTF8.GetString(new byte[] { 0xEF, 0xBB, 0xBF });
            xmlString = bom + stringWriter.ToString();
        }
             
        return xmlString;
    }
}


