using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Calculo_Credito;
using TheatricalPlayersRefactoringKata.Factory;
using System.Xml;
using System.IO;
using System.Xml.Schema;

namespace TheatricalPlayersRefactoringKata.XML;

public class SerializaçaoDadosXml : ISerializaçaoDados
{
    private readonly IPlayTypeFactory _factory;
    private readonly ICalculoCreditoEspectador _creditoEspectador;

    public SerializaçaoDadosXml(IPlayTypeFactory factory, ICalculoCreditoEspectador creditoEspectador)
    {
        _factory = factory;
        _creditoEspectador = creditoEspectador;
    }

    public string SerializandoDados(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0.00m;
        var volumeCredits = 0;
        var caminhoXml = "TheatricalPlayers.xml";

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.ConformanceLevel = ConformanceLevel.Document;

        using (StringWriter stringWriter = new StringWriter())
        {
            using (XmlWriter writer = XmlWriter.Create(caminhoXml, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Statement");
                writer.WriteElementString("Customer", invoice.Customer);
                writer.WriteStartElement("items");

                foreach (var perf in invoice.Performances)
                {
                    var play = plays[perf.PlayId];
                    decimal thisAmount = 0.00m;
                    var credits = 0;

                    var calculoEmRelacaoAoTipo = _factory.FabricaDeTypes(play);

                    thisAmount += calculoEmRelacaoAoTipo.CalculaValoresBase(perf, play);

                    credits += _creditoEspectador.CalculaCredito(perf, play);

                    writer.WriteStartElement("item");
                    writer.WriteElementString("AmountOwed", Convert.ToString(thisAmount));
                    writer.WriteElementString("EarnedCredits", Convert.ToString(credits));
                    writer.WriteElementString("Seats", Convert.ToString(perf.Audience));
                    writer.WriteEndElement();

                    totalAmount += thisAmount;
                    volumeCredits += credits;
                }

                writer.WriteEndElement();
                writer.WriteElementString("AmountOwed", Convert.ToString(totalAmount));
                writer.WriteElementString("EarnedCredits", Convert.ToString(volumeCredits));
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            ValidaçaoXmlSchema(stringWriter.ToString());

            return stringWriter.ToString();
        }
    }

    private void ValidaçaoXmlSchema(string conteudoXml)
    {
        XmlSchemaSet schema = new XmlSchemaSet();

        XmlReaderSettings readerSettings = new XmlReaderSettings();
        readerSettings.Schemas = schema;

        using(StringReader stringReader = new StringReader(conteudoXml))
        {
            using(XmlReader reader = XmlReader.Create(stringReader, readerSettings))
            {
                while (reader.Read()) { }
            }
        }
    }
}
